using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Extension;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform; //The object camera will follow
    public InputManager inputManager; 
    public Vector3 cameraFollowVelocity = Vector3.zero;
    public Vector3 cameraVectorPosition; 
    public Transform cameraPivot; //The object the camera use to pivot (look up and down)
    public Transform cameraTransform; //The transform of the camera object actual in the scenes
    public float defaultPosition;
    public float minimumCollisionOffSet = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float lookAngle = 0; //camera look up and down
    public float pivotAngle = 0; //camera look left and right
    public float cameraLookSpeed = 2.0f;
    public float cameraPivotSpeed = 2.0f;
    public float minimunPivotAngle = -35;
    public float maximumPivotAngle = 35;
    public float cameraCollisionRadius = 2;
    public LayerMask collisionLayer; //Layer camera collide with
    public float cameraCollisionOffSet = 0.2f;
    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z; 
    }

    public void HandleAllCamera()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    // Follow object player control
    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position,
            ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }
    // Rotate camera follow pointer in screen
    public void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY* cameraPivotSpeed);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
        pivotAngle = Mathf.Clamp(pivotAngle, minimunPivotAngle, maximumPivotAngle);
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;

    }
    // Camera collide with layer
    public void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit,
                Mathf.Abs(targetPosition),collisionLayer))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =-  (distance - cameraCollisionOffSet);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition,0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
