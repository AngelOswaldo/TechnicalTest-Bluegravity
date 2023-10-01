using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerEconomy : MonoBehaviour
{
    public static int CurrentMoney = 0;
    [SerializeField] private TextMeshProUGUI _moneyAmountText;

    public static Action<int> SpendMoneyEvent;
    public static Action<int> EarnMoneyEvent;

    private void OnEnable()
    {
        SpendMoneyEvent += SpendMoney;
        EarnMoneyEvent += EarnMoney;
    }

    private void OnDisable()
    {
        SpendMoneyEvent -= SpendMoney;
        EarnMoneyEvent -= EarnMoney;
    }

    private void UpdateMoneyUI()
    {
        _moneyAmountText.SetText($"{CurrentMoney}");
    }

    private void SpendMoney(int value)
    {
        if(value < CurrentMoney)
        {
            CurrentMoney -= value;
            UpdateMoneyUI();
        }
    }

    private void EarnMoney(int value)
    {
        CurrentMoney += value;
        UpdateMoneyUI();
    }
}
