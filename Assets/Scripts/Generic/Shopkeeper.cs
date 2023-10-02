using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    /// <summary>
    /// Opens the in-game shop UI and prevents the player from walking.
    /// </summary>
    public override void Interact()
    {
        // Invoke the event to open the in-game shop UI.
        ShopManager.OpenUIShopEvent?.Invoke();

        // Invoke the event to prevent the player from walking.
        PlayerController.CanPlayerWalkEvent?.Invoke(false);
    }
}
