using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManagerEnemy : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void TargetAnimation(string targetAnimation, bool isInteracting)
    {
        _animator.SetBool("isInteracting",isInteracting);
        _animator.CrossFade(targetAnimation,0.2f);
    }
}
