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
        CloseChestEvent += CloseChest;
    }

    private void OnDisable()
    {
        CloseChestEvent -= CloseChest;
    }

    public override void Interact()
    {
        WardrobeManager.OpenUIWarobeEvent?.Invoke();
        PlayerController.CanPlayerWalkEvent?.Invoke(false);
        _spriteRenderer.sprite = _openChest;
    }

    private void CloseChest()
    {
        _spriteRenderer.sprite = _closeChest;
    }
}
