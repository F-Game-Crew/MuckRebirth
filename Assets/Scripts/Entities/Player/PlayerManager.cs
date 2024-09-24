using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator _animator;
    public CameraManager cameraManager;
    public InputManager inputManager;
    public PlayerLocation playerLocotion;
    public bool isInteracting;
    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        inputManager = GetComponent<InputManager>();
        playerLocotion = GetComponent<PlayerLocation>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputManager.HandleAllInput();
    }

    private void FixedUpdate()
    {
        playerLocotion.HandleAllMovement();
        
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCamera();

        isInteracting = _animator.GetBool("isInteracting");
        playerLocotion.isJumping = _animator.GetBool("isJumping");
        _animator.SetBool("isGrounded",playerLocotion.isGrounded);
    }
}
