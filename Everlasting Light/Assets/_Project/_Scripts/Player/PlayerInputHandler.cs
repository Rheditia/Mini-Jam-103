using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction attackAction;
    InputAction interractAction;
    InputAction jumpAction;

    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool InterractInput { get; private set; }
    public bool JumpInput { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];
        interractAction = playerInput.actions["Interract"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        attackAction.performed += OnAttackInput;
        attackAction.canceled += OnAttackInput;

        interractAction.performed += OnInterractInput;
        interractAction.canceled += OnInterractInput;

        jumpAction.performed += OnJumpInput;
        jumpAction.canceled += OnJumpInput;
    }

    private void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        attackAction.performed -= OnAttackInput;
        attackAction.canceled -= OnAttackInput;

        interractAction.performed -= OnInterractInput;
        interractAction.canceled -= OnInterractInput;

        jumpAction.performed -= OnJumpInput;
        jumpAction.canceled -= OnJumpInput;
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValue<float>() == 1;
    }

    private void OnInterractInput(InputAction.CallbackContext context)
    {
        InterractInput = context.ReadValue<float>() == 1;
        //Debug.Log(InterractInput);
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        AttackInput = context.ReadValue<float>() == 1;
        //Debug.Log(AttackInput);
    }
}
