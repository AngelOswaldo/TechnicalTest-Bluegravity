using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    public static Action<bool> WalkEvent;
    public static Action<bool> BodyFlipEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        WalkEvent += WalkAnimation;
        BodyFlipEvent += FlipX;
    }

    private void OnDisable()
    {
        WalkEvent -= WalkAnimation;
        BodyFlipEvent -= FlipX;
    }

    private void FlipX(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void WalkAnimation(bool value)
    {
        _animator.SetBool("isWalking", value);
    }
}
