using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    public PlayerManager _playerManager;
    public InputManager inputManager;
    public AnimatorManager _animatorManager;
    public Transform cameraObject;
    public Rigidbody playerRigidbody;
    
    [Header("Movement Speed")]
    public Vector3 movementDirection;
    public bool isRunning;
    public float rotationSpeed = 2.0f;
    public float runSpeed;
    public float movementSpeed = 5.0f;
    [Header("Jumping")]
    public float jumpHeight = 3;
    public float gravityIndentity = -20;
    public bool isJumping;
    public float raycastHeightOffSet = 0.5f;
    [Header("Falling")]
    public float fallingVelocity;
    public float leapingVelocity;
    public float inAirTime;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool isLand;
    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (_playerManager.isInteracting)
        {
            return;
        }
        HandleMovement();
        HandleRotation();   
    }
    public void HandleMovement()
    {
        movementDirection = cameraObject.forward * inputManager.horizontal;
        movementDirection = movementDirection + cameraObject.right * inputManager.vertical;
        movementDirection.y = 0;
        movementDirection.Normalize();
        
        if (isRunning)
        {
            
            movementDirection *= runSpeed;
        }
        else
        {
            movementDirection *= movementSpeed;
        }
        Vector3 moveVelocity = movementDirection;
        playerRigidbody.velocity = moveVelocity;
    }
    public void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.horizontal;
        targetDirection = targetDirection + cameraObject.right * inputManager.vertical;
        targetDirection.y = 0;
        targetDirection.Normalize();
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            _animatorManager.TargetAnimation("Jump",true);
            float jumpVelocity = Mathf.Sqrt(-2 * gravityIndentity * jumpHeight);
            Vector3 playerVelocity = movementDirection;
            playerVelocity.y = jumpVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }

    public void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastHeightOffSet;
        if (!isGrounded && !isJumping)
        {
            if (!_playerManager.isInteracting)
            {
                _animatorManager.TargetAnimation("Falling",true);
            }

            inAirTime = inAirTime + Time.deltaTime;
            playerRigidbody.AddForce(Vector3.forward * leapingVelocity * inAirTime);
            playerRigidbody.AddForce(-Vector3.up * inAirTime * fallingVelocity);
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            // if (!isGrounded && !_playerManager.isInteracting)
            // {
            //     _animatorManager.TargetAnimation("Land",true);
            // }

            inAirTime = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            
        }
    }
}
