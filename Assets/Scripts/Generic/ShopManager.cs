using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

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
    }
}
