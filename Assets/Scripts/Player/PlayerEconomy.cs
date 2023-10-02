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

    private void Start()
    {
        EarnMoneyEvent.Invoke(200); // Start with an initial amount of money
    }

    private void OnEnable()
    {
        SpendMoneyEvent += SpendMoney; // Subscribe to the SpendMoneyEvent
        EarnMoneyEvent += EarnMoney; // Subscribe to the EarnMoneyEvent
    }

    private void OnDisable()
    {
        SpendMoneyEvent -= SpendMoney; // Unsubscribe from the SpendMoneyEvent
        EarnMoneyEvent -= EarnMoney; // Unsubscribe from the EarnMoneyEvent
    }

    /// <summary>
    /// Update the money UI text to display the current amount of money.
    /// </summary>
    private void UpdateMoneyUI()
    {
        _moneyAmountText.SetText($"{CurrentMoney}");
    }

    /// <summary>
    /// Spend money by subtracting a specified value from the current money.
    /// </summary>
    /// <param name="value">The amount of money to spend.</param>
    private void SpendMoney(int value)
    {
        if (value <= CurrentMoney)
        {
            CurrentMoney -= value;
            UpdateMoneyUI();
        }
    }

    /// <summary>
    /// Earn money by adding a specified value to the current money.
    /// </summary>
    /// <param name="value">The amount of money to earn.</param>
    private void EarnMoney(int value)
    {
        CurrentMoney += value;
        UpdateMoneyUI();
    }
}
