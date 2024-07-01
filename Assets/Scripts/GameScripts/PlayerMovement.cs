using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ENUM_PlayerMoveState
{
    idle,
    walking,
    running,
    jumping,
    dashing
}

public class PlayerMovement : NetworkBehaviour
{
    [Header("Basic Data")]
    [SerializeField] float gravityValue = -9.81f;
    public ENUM_PlayerMoveState moveState;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float groundCheckDistance;
    Vector3 velocity;
    Vector3 dir;
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeedMultiplier;
    [SerializeField] float runningStaminaCost;
    [SerializeField] bool isRunning;
    [Header("Jumping")]
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float jumpStaminaCost;
    [SerializeField] Vector2 input;
    [Header("Dash Data")]
    [SerializeField] float dashDuration;
    [SerializeField] float dashStrength;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashStaminaCost;
    float dashCooldownTimer;
    Vector3 dashDirection;
    float dashTimer;
    bool dashOnCooldown;

    [Header("Reference")]
    public CharacterController controller;
    public PlayerStats stats;
    public PlayerData2 data;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
        {
            Debug.Log("Not the owner, name is " + gameObject.name);
            return;
        }

        GroundCheck();
        ActAccordingToState();
        DashCooldownTimer();
    }
    void ActAccordingToState()
    {
        //Debug.Log("Acting According to State");
        switch (moveState)
        {
            case ENUM_PlayerMoveState.idle:
                Idle();
                break;
            case ENUM_PlayerMoveState.walking:
                Walking();
                break;
            case ENUM_PlayerMoveState.jumping:
                Jumping();
                break;
            case ENUM_PlayerMoveState.dashing:
                Dashing();
                break;
            default:
                break;
        }

    }
    void TryToChangeState(ENUM_PlayerMoveState newState)
    {
        if (moveState == newState)
            return;

        //Debug.Log("New state = " + newState);
        //moveState = newState;

        /*
        if(moveState != ENUM_PlayerMoveState.dashing)
        {
            moveState = newState;
        }
        */
        
        if (moveState != ENUM_PlayerMoveState.dashing && moveState != ENUM_PlayerMoveState.jumping)
        {
            //Debug.Log("New state = " + newState);
            moveState = newState;
        }
        if(moveState == ENUM_PlayerMoveState.jumping && newState == ENUM_PlayerMoveState.dashing)
        {
            moveState = newState;
        }
        if(moveState == ENUM_PlayerMoveState.dashing && newState == ENUM_PlayerMoveState.jumping)
        {
            //velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue) - 6;
            velocity.y = 4;
            //Jumping();
        }
        
        
    }
    void FinishCurrentState(ENUM_PlayerMoveState newState)
    {
        moveState = newState;
    }

    public void ActionMoving(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            input = context.ReadValue<Vector2>();
            TryToChangeState(ENUM_PlayerMoveState.walking);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            input = Vector2.zero;
            TryToChangeState(ENUM_PlayerMoveState.idle);
        }
    }
    public void ActionRunning(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            isRunning = true;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
        }
    }
    public void ActionJump(InputAction.CallbackContext context)
    {
        if (groundedPlayer == false)
            return;
        if (moveState == ENUM_PlayerMoveState.jumping)
            return;
        if (stats.CheckIfCanUseStamina(jumpStaminaCost) == false)
            return;

        velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        Debug.Log("Velocity = " + velocity.y);
        TryToChangeState(ENUM_PlayerMoveState.jumping);
        Jumping();
        Debug.Log("After Jumping Called");
    }
    public void ActionDash(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed)
        {
            Debug.Log("Dash not performed");
            return;
        }

        Debug.Log("Trying to Dash");
        if (dashOnCooldown == true)
        {
            //Debug.LogWarning("Implement icon flash, to show its on CD");
            Debug.Log("Dash on CD");
            return;
        }

        if (stats.CheckIfCanUseStamina(dashStaminaCost) == false)
        {
            Debug.Log("Not enough Stamina");
            return;
        }

        if (input == Vector2.zero)
        {
            dashDirection.x = 0;
            dashDirection.z = 1;
        }
        else
        {
            dashDirection.x = input.x;
            dashDirection.z = input.y;
        }

        dashDirection = transform.TransformDirection(dashDirection);
        dashDirection *= dashStrength;
        dashDirection *= Time.deltaTime;
        Debug.Log("dash direction = " + dashDirection);
        dashTimer = 0;
        dashOnCooldown = true;
        TryToChangeState(ENUM_PlayerMoveState.dashing);
    }

    void Idle()
    {
        MoveAccordingToGravity();
        return;
    }
    void Walking()
    {
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);

        if(isRunning == true)
        {
            dir *= runSpeedMultiplier;
        }

        controller.Move(dir);
        MoveAccordingToGravity();
    }
    void Dashing()
    {
        dashTimer += Time.deltaTime;
        //Debug.Log("Dir = " + dashDirection);
        controller.Move(dashDirection);
        //controller.SimpleMove(dashDirection);
        if (dashTimer >= dashDuration)
        {
            dashTimer = 0;
            //CompleteCurrentState(ENUM_PlayerMoveState.walking);
            if (input == Vector2.zero)
            {
                FinishCurrentState(ENUM_PlayerMoveState.idle);
                //TryToChangeState(ENUM_PlayerMoveState.idle);
            }
            else
            {
                FinishCurrentState(ENUM_PlayerMoveState.walking);
                //TryToChangeState(ENUM_PlayerMoveState.walking);
            }
        }
        MoveAccordingToGravity();
    }
    void Jumping()
    {
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);

        controller.Move(dir);
        MoveAccordingToGravity();

        //if(groundedPlayer == true)
        if(velocity.y <= 0 && groundedPlayer)
        {
            Debug.Log("Jumping finished,");
            if (input == Vector2.zero)
            {
                FinishCurrentState(ENUM_PlayerMoveState.idle);
                //TryToChangeState(ENUM_PlayerMoveState.idle);
            }
            else
            {
                FinishCurrentState(ENUM_PlayerMoveState.walking);
                //TryToChangeState(ENUM_PlayerMoveState.walking);
            }
        }
    }

    void GroundCheck()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if(groundedPlayer == true && velocity.y <= 0)
        {
            velocity.y = 0;
        }
    }
    void MoveAccordingToGravity()
    {
        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void DashCooldownTimer()
    {
        if (dashOnCooldown == true)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                dashCooldownTimer = 0;
                dashOnCooldown = false;
            }
        }
    }
}
