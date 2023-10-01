using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    [SerializeField] private GameObject _shopUI;

    public override void Interact()
    {
        Debug.Log("Interact...");
        //_shopUI.SetActive(true);
        //PlayerController.CanPlayerWalkEvent?.Invoke(false);
    }
}
