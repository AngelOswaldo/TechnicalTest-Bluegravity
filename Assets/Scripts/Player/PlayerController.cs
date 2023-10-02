using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private bool _canWalk = true;
    private bool _isWalking = false;
    private bool _bodyFlip = false;
    private bool _isFacingRight = true;

    public static Action<bool> CanPlayerWalkEvent;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        CanPlayerWalkEvent += CanWalk; // Subscribe to the CanPlayerWalkEvent
    }

    private void OnDisable()
    {
        CanPlayerWalkEvent -= CanWalk; // Unsubscribe from the CanPlayerWalkEvent when disabled
    }

    private void FixedUpdate()
    {
        if (!_canWalk)
        {
            _rb.velocity = Vector2.zero; // Stop player movement
            PlayerAnimation.WalkEvent?.Invoke(false); // Notify animation that the player stopped walking
            return; // Exit the FixedUpdate early if walking is not allowed
        }

        Movement();
    }

    /// <summary>
    /// Handle player movement.
    /// </summary>
    private void Movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        _movementInput = new Vector2(horizontalMovement, verticalMovement).normalized; // Normalize input for consistent speed

        _isWalking = _movementInput.magnitude > 0; // Check if the player is walking based on input magnitude

        PlayerAnimation.WalkEvent?.Invoke(_isWalking); // Notify animation about walking state

        if (_isWalking)
        {
            if (horizontalMovement < 0)
            {
                _isFacingRight = false; // Character is facing left
            }
            else if (horizontalMovement > 0)
            {
                _isFacingRight = true; // Character is facing right
            }

            _bodyFlip = !_isFacingRight; // Determine if body needs to be flipped
            PlayerAnimation.BodyFlipEvent?.Invoke(_bodyFlip); // Notify animation about body flip
        }

        _rb.velocity = _movementInput * _speed; // Apply movement velocity
    }

    /// <summary>
    /// Control whether the player can walk.
    /// </summary>
    /// <param name="value">If true, the player can walk.</param>
    private void CanWalk(bool value)
    {
        _canWalk = value; // Update the walk permission based on the event
    }
}
