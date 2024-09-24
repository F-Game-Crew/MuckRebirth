using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    [SerializeField] private PlayerControl _playerControl;
    private PlayerLocation _playerLocotion;
    private AnimatorManager _animatorManager;
    private float _moveAmount;
    [SerializeField] private Vector2 movementInput;
    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;
    public float horizontal;
    public float vertical;
    public bool jumpInput;
    public bool runInput;
    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerLocotion = GetComponent<PlayerLocation>();
    }

    private void OnEnable()
    {
        if (_playerControl == null)
        {
            _playerControl = new PlayerControl();
            _playerControl.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            _playerControl.PlayerMovement.Jump.performed += i =>  jumpInput = true;
            _playerControl.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            _playerControl.PlayerActions.Running.performed += i => runInput = true;
            _playerControl.PlayerActions.Running.canceled += i => runInput = false;
        }
        _playerControl.Enable();
        
    }

    public void HandleMoveInput()
    {
        horizontal = movementInput.y;
        vertical = movementInput.x;
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        
        _animatorManager.UpdateAnimatorValues(0,_moveAmount);
    }

    public void HandleAllInput()
    {
        HandleMoveInput();
        HandleJumpingInput();
        HandleRunningInput();
    }
    private void OnDisable()
    {
        _playerControl.Disable();
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            _playerLocotion.HandleJump();
        }
    }

    public void HandleRunningInput()
    {
        if (runInput)
        {
            _playerLocotion.isRunning = true;
        }
        else
        {
            _playerLocotion.isRunning = false;
        }
    }
    
}
