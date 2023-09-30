using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private Rigidbody2D _rb;

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
        float verticalMovevement = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2 (horizontalMovement, verticalMovevement);

        movement.Normalize();

        _rb.velocity = movement * _speed;
    }
}
