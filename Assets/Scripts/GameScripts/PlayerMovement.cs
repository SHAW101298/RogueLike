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
    }
    void ActAccordingToState()
    {
        Debug.Log("Acting According to State");
        switch (moveState)
        {
            case ENUM_PlayerMoveState.idle:
                Idle();
                break;
            case ENUM_PlayerMoveState.walking:
                Walking();
                break;
            case ENUM_PlayerMoveState.jumping:
                
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
        moveState = newState;

        if (moveState != ENUM_PlayerMoveState.dashing && moveState != ENUM_PlayerMoveState.jumping)
        {
            //Debug.Log("New state = " + newState);
            moveState = newState;
        }
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
    public void ActionJump()
    {
        if (groundedPlayer == false)
            return;
        if (moveState == ENUM_PlayerMoveState.jumping)
            return;
        if (stats.CheckIfCanUseStamina(jumpStaminaCost) == false)
            return;

        velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        //Debug.Log("Velocity = " + velocity.y);
        TryToChangeState(ENUM_PlayerMoveState.jumping);
    }

    void Walking()
    {
        dir.x = input.x;
        dir.z = input.y;
        dir *= moveSpeed * Time.deltaTime;
        dir = transform.TransformDirection(dir);

        controller.Move(dir);
        MoveAccordingToGravity();
    }
    void Idle()
    {
        MoveAccordingToGravity();
        return;
    }

    void GroundCheck()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1.35f, groundLayer);
    }
    void MoveAccordingToGravity()
    {
        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
