using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private float _currentShirtID = 0;
    [SerializeField] private float _currentPantsID = 0;

    private int[] shirtsUnlocked = new int[0];
    private int[] pantsUnlocked = new int[0];

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

    public void WearShirt(bool value)
    {
        if (value)
        {
            _animator.SetLayerWeight(1, 1f);
        }
        else
        {
            _animator.SetLayerWeight(1, 0f);
        }
    }

    public void WearPants(bool value)
    {
        if (value)
        {
            _animator.SetLayerWeight(2, 1f);
        }
        else 
        { 
            _animator.SetLayerWeight(2,0f);
        }
    }

    public void ChangeShirt(int value)
    {
        _animator.SetFloat("shirtID", value);
        _currentShirtID = value;
    }

    public void ChangePants(int value)
    {
        _animator.SetFloat("pantsID", value);
        _currentPantsID = value;
    }
}
