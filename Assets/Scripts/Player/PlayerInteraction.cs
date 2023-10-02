using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Interactable _currentInteractable;
    [SerializeField] private bool _canInteract = false;

    public static Action EnableInteractionEvent;

    private void OnEnable()
    {
        EnableInteractionEvent += EnableInteraction;
    }

    private void OnDisable()
    {
        EnableInteractionEvent -= EnableInteraction;
    }

    private void Update()
    {
        if (!_canInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
            _canInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _currentInteractable = collision?.GetComponent<Interactable>();
            if(_currentInteractable != null )
            {
                _canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _currentInteractable = null;
            _canInteract = false;
        }
    }

    private void EnableInteraction()
    {
        _canInteract = true;
    }
}
