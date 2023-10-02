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
        EnableInteractionEvent += EnableInteraction; // Subscribe to the EnableInteractionEvent
    }

    private void OnDisable()
    {
        EnableInteractionEvent -= EnableInteraction; // Unsubscribe from the EnableInteractionEvent
    }

    private void Update()
    {
        if (!_canInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact(); // Trigger the interaction with the current interactable
            _canInteract = false; // Disable further interaction until re-enabled
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _currentInteractable = collision?.GetComponent<Interactable>(); // Get the Interactable component from the collider
            if (_currentInteractable != null)
            {
                _canInteract = true; // Enable interaction when a valid interactable object is detected
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _currentInteractable = null; // Clear the reference to the current interactable
            _canInteract = false; // Disable interaction when the player moves away from the interactable object
        }
    }

    /// <summary>
    /// Enable interaction, allowing the player to interact with objects.
    /// </summary>
    private void EnableInteraction()
    {
        _canInteract = true; // Enable interaction
    }
}
