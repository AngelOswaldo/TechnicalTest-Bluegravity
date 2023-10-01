using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    public override void Interact()
    {
        ShopManager.OpenUIShopEvent?.Invoke();
        PlayerController.CanPlayerWalkEvent?.Invoke(false);
    }
}
