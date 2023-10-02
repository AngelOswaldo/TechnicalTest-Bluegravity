using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private ClothesData _clothesData;

    public static Action OpenUIShopEvent;

    private void OnEnable()
    {
        OpenUIShopEvent += OpenUIShop;
    }

    private void OnDisable()
    {
        OpenUIShopEvent -= OpenUIShop;
    }

    private void OpenUIShop()
    {
        _shopPanel.SetActive(true);
    }

    public void CloseUIShop()
    {
        _shopPanel.SetActive(false);
        PlayerController.CanPlayerWalkEvent?.Invoke(true);
        PlayerInteraction.EnableInteractionEvent?.Invoke();
    }

    public void BuyItem(string code)
    {
        if(!_clothesData.IsItemUnlocked(code))
        {
            if(_clothesData.BuyClothes(code, PlayerEconomy.CurrentMoney))
            {
                PlayerEconomy.SpendMoneyEvent?.Invoke(_clothesData.GetItemPrice(code));
            }
        }
    }

    public void SellItem(string code)
    {
        if (_clothesData.IsItemUnlocked(code))
        {
            _clothesData.SellClothes(code);
        }
    }
}
