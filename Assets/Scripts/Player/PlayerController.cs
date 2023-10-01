using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private bool _isWalking = false;
    private bool _bodyFlip = false;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        _movementInput = new Vector2(horizontalMovement, verticalMovement).normalized;

        _isWalking = _movementInput.magnitude > 0;

        PlayerAnimation.WalkEvent?.Invoke(_isWalking);

        if (_isWalking)
        {
            if (horizontalMovement < 0)
            {
                _isFacingRight = false;
            }
            else if (horizontalMovement > 0)
            {
                _isFacingRight = true;
            }

            _bodyFlip = !_isFacingRight;
            PlayerAnimation.BodyFlipEvent?.Invoke(_bodyFlip);
        }

        _rb.velocity = _movementInput * _speed;
    }
}
