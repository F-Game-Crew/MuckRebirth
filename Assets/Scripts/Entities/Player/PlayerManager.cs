using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator _animator;
    public CameraManager cameraManager;
    public InputManager inputManager;
    public PlayerLocation playerLocation;
    public bool isInteracting;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        inputManager = GetComponent<InputManager>();
        playerLocation = GetComponent<PlayerLocation>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputManager.HandleAllInput();
    }

    private void FixedUpdate()
    {
        playerLocation.HandleAllMovement();

    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCamera();

        isInteracting = _animator.GetBool("isInteracting");
        playerLocation.isJumping = _animator.GetBool("isJumping");
        _animator.SetBool("isGrounded", playerLocation.isGrounded);
    }
}
