using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    
    public Animator _animator;
    private int _horizontal;
    private int _vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
    }
    public void TargetAnimation(string targetAnimation, bool isInteracting)
    {
        _animator.SetBool("isInteracting",isInteracting);
        _animator.CrossFade(targetAnimation,0.2f);
    }
    public void UpdateAnimatorValues(float horizontalMove, float verticalMove)
    {
        float snappedHorizontal;
        float snappedVertical;
        
        #region Snapped Horizontal
        if (horizontalMove > 0 && horizontalMove < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMove > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMove < 0 && horizontalMove > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMove < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMove > 0 && verticalMove < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMove > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMove < 0 && verticalMove > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMove < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        
        _animator.SetFloat(_horizontal,snappedHorizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, snappedVertical, 0.1f,Time.deltaTime);
    }
}
