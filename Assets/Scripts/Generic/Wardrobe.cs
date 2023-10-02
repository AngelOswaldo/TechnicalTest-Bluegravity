using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactable
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _openChest;
    [SerializeField] private Sprite _closeChest;

    public static Action CloseChestEvent;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Register the CloseChest method to be called when CloseChestEvent is invoked.
        CloseChestEvent += CloseChest;
    }

    private void OnDisable()
    {
        // Unregister the CloseChest method from CloseChestEvent to avoid memory leaks.
        CloseChestEvent -= CloseChest;
    }

    /// <summary>
    /// Opens the wardrobe and triggers the wardrobe UI to open.
    /// </summary>
    public override void Interact()
    {
        // Invoke the OpenUIWarobeEvent to open the wardrobe UI.
        WardrobeManager.OpenUIWarobeEvent?.Invoke();

        // Disable player movement.
        PlayerController.CanPlayerWalkEvent?.Invoke(false);

        // Change the wardrobe sprite to the open chest.
        _spriteRenderer.sprite = _openChest;
    }

    /// <summary>
    /// Called when the player closes the wardrobe UI.
    /// </summary>
    private void CloseChest()
    {
        // Change the wardrobe sprite to the closed chest.
        _spriteRenderer.sprite = _closeChest;
    }
}
