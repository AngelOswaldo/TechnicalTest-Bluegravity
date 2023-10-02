using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private ClothesData _clothesData;

    public static Action OpenUIShopEvent;  // Event to open the shop UI.

    private void OnEnable()
    {
        OpenUIShopEvent += OpenUIShop; // Subscribe to the event when this script is enabled.
    }

    private void OnDisable()
    {
        OpenUIShopEvent -= OpenUIShop; // Unsubscribe from the event when this script is disabled.
    }

    /// <summary>
    /// Opens the shop UI by enabling the shop panel.
    /// </summary>
    private void OpenUIShop()
    {
        _shopPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the shop UI by disabling the shop panel, enabling player movement, and enabling player interaction.
    /// </summary>
    public void CloseUIShop()
    {
        _shopPanel.SetActive(false);
        PlayerController.CanPlayerWalkEvent?.Invoke(true);
        PlayerInteraction.EnableInteractionEvent?.Invoke();
    }

    /// <summary>
    /// Attempts to buy an item with the given item code if it is not already unlocked and the player has enough money.
    /// </summary>
    /// <param name="code">The code of the item to buy.</param>
    public void BuyItem(string code)
    {
        if (!_clothesData.IsItemUnlocked(code))
        {
            if (_clothesData.BuyClothes(code, PlayerEconomy.CurrentMoney))
            {
                PlayerEconomy.SpendMoneyEvent?.Invoke(_clothesData.GetItemPrice(code));
            }
        }
    }

    /// <summary>
    /// Sells an item with the given item code if it is already unlocked.
    /// </summary>
    /// <param name="code">The code of the item to sell.</param>
    public void SellItem(string code)
    {
        if (_clothesData.IsItemUnlocked(code))
        {
            _clothesData.SellClothes(code);
        }
    }
}
