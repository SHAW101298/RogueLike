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
    dashing,
    jumpDash
}

public class PlayerMovement : NetworkBehaviour
{
    [Header("Basic Data")]
    [SerializeField] float gravityValue = -9.81f;
    public ENUM_PlayerMoveState moveState;
    [SerializeField] GameObject groundCheckTransform;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float groundCheckDistance;
    [SerializeField]Vector3 velocity;
    Vector3 dir;
    bool blockedMovement;
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
    [SerializeField]float dashTimer;
    [SerializeField] bool dashOnCooldown;

    [Header("Reference")]
    public CharacterController controller;
    public PlayerStats stats;
    public PlayerData data;
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
        {
            Debug.Log("Not the owner, name is " + gameObject.name);
            return;
        }

        if(blockedMovement == true)
        {
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
            case ENUM_PlayerMoveState.jumpDash:
                JumpDash();
                break;
            default:
                break;
        }

    }
    bool TryToChangeState(ENUM_PlayerMoveState newState)
    {
        if (moveState == newState)
        {
            return false;
        }

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
            return true;
        }


        if (moveState == ENUM_PlayerMoveState.jumping && newState == ENUM_PlayerMoveState.dashing)
        {
            moveState = ENUM_PlayerMoveState.jumpDash;
            return true;
        }
        if(moveState == ENUM_PlayerMoveState.dashing && newState == ENUM_PlayerMoveState.jumping)
        {
            moveState = ENUM_PlayerMoveState.jumpDash;
            return true;
            //velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        return false;
        /*
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
        */

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
        if(context.phase != InputActionPhase.Performed)
        {
            return;
        }

        //Debug.Log("Action Jump Approved");
        if (groundedPlayer == false)
            return;
        if (moveState == ENUM_PlayerMoveState.jumping)
            return;
        if (stats.CheckIfCanUseStamina(jumpStaminaCost) == false)
            return;


        //Debug.Log("Current Velocity = " + velocity.y);
        bool check = TryToChangeState(ENUM_PlayerMoveState.jumping);
        if (check == false)
            return;
        velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //Debug.Log("New Velocity = " + velocity.y);
        stats.ReduceStamina(jumpStaminaCost);
        Jumping();
        //Debug.Log("After Jumping Called");
    }
    public void ActionDash(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed)
        {
            //Debug.Log("Dash not performed");
            return;
        }

        //Debug.Log("Trying to Dash");
        if (dashOnCooldown == true)
        {
            //Debug.LogWarning("Implement icon flash, to show its on CD");
            //Debug.Log("Dash on CD");
            return;
        }

        if (stats.CheckIfCanUseStamina(dashStaminaCost) == false)
        {
            //Debug.Log("Not enough Stamina");
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


        // Dane do dasha ustawione
        dashDirection = transform.TransformDirection(dashDirection);
        dashDirection *= dashStrength;
        dashDirection *= Time.deltaTime;
        dashTimer = 0;

        // Jeœli przesz³o pomyœlnie test, rozpoczyna siê dasz
        bool check = TryToChangeState(ENUM_PlayerMoveState.dashing);
        if (check == false)
            return;
        stats.ReduceStamina(dashStaminaCost);
        //Debug.Log("dash direction = " + dashDirection);
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
            dashOnCooldown = true;
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
            //Debug.Log("Jumping finished,");
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
    void JumpDash()
    {
        // Czy wciaz mozna Dashowac
        if(dashTimer < dashDuration && dashOnCooldown == false)
        {
            //Debug.Log("robimy dash w powietrzu");
            dashTimer += Time.deltaTime;
            controller.Move(dashDirection);
        }
        // Koniec Dasha
        if (dashTimer >= dashDuration)
        {
            dashTimer = 0;
            dashOnCooldown = true;
        }
        // Skok wciaz ciagnie w dó³
        MoveAccordingToGravity();

        // Ruch podczas skoku
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);
        controller.Move(dir);

        //if(groundedPlayer == true)
        if (velocity.y <= 0 && groundedPlayer && dashTimer == 0)
        {
            //Debug.Log("JumpDash finished,");
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
        groundedPlayer = Physics.Raycast(groundCheckTransform.transform.position, Vector3.down, groundCheckDistance, groundLayer);
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

    public void BlockMovement()
    {
        blockedMovement = true;
    }
    public void AllowMovement()
    {
        blockedMovement = false;
    }
    public void SetMovementSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
    public Vector3 GetMoveDir()
    {
        Debug.Log("Move Dir = " + dir);
        return dir;
    }
}
