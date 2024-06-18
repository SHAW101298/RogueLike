using System.Collections;
using System.Collections.Generic;
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
public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Data")]
    [SerializeField] float gravityValue = -9.81f;
    public ENUM_PlayerMoveState moveState;
    bool groundedPlayer;
    Vector3 velocity;
    Vector3 dir;
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeedMultiplier;
    [SerializeField] float runningStaminaCost;
    bool isRunning;
    [Header("Jumping")]
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float jumpStaminaCost;
    [SerializeField]Vector2 input;
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
    public PlayerData data;
    public LayerMask groundLayer;

    [Header("Debug")]
    public bool doJump;

    public void ActionMoving(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            input = context.ReadValue<Vector2>();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            input = Vector2.zero;
        }
        TryToChangeState(ENUM_PlayerMoveState.walking);
    }
    public void ActionRunning(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            isRunning = true;
            if(moveState == ENUM_PlayerMoveState.walking)
            {
                //TryToChangeState(ENUM_PlayerMoveState.running);
            }
            Debug.Log("true RUNNING");
        }
        if(context.phase == InputActionPhase.Canceled)
        {

            Debug.Log("False");
            isRunning = false;
            if(moveState == ENUM_PlayerMoveState.dashing)
            {
                return;
            }
            TryToChangeState(ENUM_PlayerMoveState.walking);
        }
    }
    public void ActionDash(InputAction.CallbackContext context)
    {
        //Debug.Log("dash phase = " + context.phase);
        if(context.phase == InputActionPhase.Performed)
        {
            if(dashOnCooldown == true)
            {
                //Debug.LogWarning("Implement icon flash, to show its on CD");
                return;
            }
            if(stats.CheckIfCanUseStamina(dashStaminaCost) == false)
            {
                //Debug.LogWarning("Implement icon flash, to show lack of Stamina");
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
            dashTimer = 0;
            dashOnCooldown = true;
            //Debug.Log("Dash Direction = " + input);
            TryToChangeState(ENUM_PlayerMoveState.dashing);
        }
    }
    public void ActionJump()
    {
        if (groundedPlayer == false)
            return;
        if (moveState == ENUM_PlayerMoveState.jumping)
            return;
        if(stats.CheckIfCanUseStamina(jumpStaminaCost) == false)
            return;

        velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        //Debug.Log("Velocity = " + velocity.y);
        TryToChangeState(ENUM_PlayerMoveState.jumping);
    }

    // Update is called once per frame
    void Update()
    {
        if(doJump == true)
        {
            doJump = false;
            ActionJump();
        }

        GroundCheck();
        DashCooldownTimer();

        switch (moveState)
        {
            case ENUM_PlayerMoveState.idle:
                Idle();
                break;
            case ENUM_PlayerMoveState.walking:
                Walking();
                break;
            case ENUM_PlayerMoveState.running:
                Running();
                break;
            case ENUM_PlayerMoveState.dashing:
                Dashing();
                break;
            case ENUM_PlayerMoveState.jumping:
                Jumping();
                break;
        }




        if (groundedPlayer && (dir.x == 0 && dir.z == 0) && velocity.y < 0)
        {
            //CompleteCurrentState(ENUM_PlayerMoveState.idle);
            TryToChangeState(ENUM_PlayerMoveState.idle);
        }
        if(groundedPlayer && velocity.y < 0)
        {
            velocity.y = 0;
            if(moveState == ENUM_PlayerMoveState.jumping)
            {
                //CompleteCurrentState(ENUM_PlayerMoveState.walking);
                TryToChangeState(ENUM_PlayerMoveState.walking);
            }
        }
    }

    void TryToChangeState(ENUM_PlayerMoveState newState)
    {
        if (moveState == newState)
            return;

        //Debug.Log("New state = " + newState);
        moveState = newState;

        if (moveState != ENUM_PlayerMoveState.dashing && moveState != ENUM_PlayerMoveState.jumping)
        {
            //Debug.Log("New state = " + newState);
            moveState = newState;
        }
    }

    void Idle()
    {
        MoveAccordingToGravity();
        return;
    }
    void Walking()
    {
        if(isRunning == true)
        {
            TryToChangeState(ENUM_PlayerMoveState.running);
            Running();
            return;
        }
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);

        controller.Move(dir);
        MoveAccordingToGravity();
    }
    void Running()
    {
        stats.CheckIfCanUseStamina(runningStaminaCost);
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * runSpeedMultiplier * Time.deltaTime;
        dir = transform.TransformDirection(dir);

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
            if(input == Vector2.zero)
            {
                TryToChangeState(ENUM_PlayerMoveState.idle);
            }
            else
            {
                TryToChangeState(ENUM_PlayerMoveState.walking);
            }
        }
        //MoveAccordingToGravity();
    }
    void Jumping()
    {
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);

        controller.Move(dir);
        MoveAccordingToGravity();
    }

    void MoveAccordingToGravity()
    {
        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void GroundCheck()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1.35f,  groundLayer);
    }
    void DashCooldownTimer()
    {
        if(dashOnCooldown == true)
        {
            dashCooldownTimer += Time.deltaTime;
            if(dashCooldownTimer >= dashCooldown)
            {
                dashCooldownTimer = 0;
                dashOnCooldown = false;
            }
        }
    }
}
