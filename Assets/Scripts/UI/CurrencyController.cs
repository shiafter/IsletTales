using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;
    [SerializeField] private int startingGold = 0;
    [SerializeField] private int startingSilver = 10;
    private int playerGold;
    private int playerSilver;

    //===CURRENCY UI===
    [SerializeField] private TMP_Text silverText;
    [SerializeField] private TMP_Text goldText;

    public event Action OnCurrencyChanged;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            playerGold = startingGold;
            playerSilver = startingSilver;

            silverText.text = playerSilver.ToString();
            goldText.text = playerGold.ToString();
        }
    }
    public int GetGold() => playerGold;
    public  int GetSilver() => playerSilver;
    public void AddCurrency(CurrencyData.CurrencyType type, int amount)
    {
        if (amount <= 0) return;

        switch (type)
        {
            case CurrencyData.CurrencyType.Silver:
                playerSilver += amount;
                UpdateUI();
                break;
            case CurrencyData.CurrencyType.Gold:
                playerGold += amount;
                UpdateUI();
                break;
            default:
                return;
        }

        OnCurrencyChanged?.Invoke();
    }
    public bool SpendCurrency(CurrencyData.CurrencyType type, int amount)
    {
        switch (type)
        {
            case CurrencyData.CurrencyType.Silver:
                if (playerSilver < amount) return false;
                playerSilver -= amount;
                UpdateUI();
                break;

            case CurrencyData.CurrencyType.Gold:
                if (playerGold < amount) return false;
                playerGold -= amount;
                UpdateUI();
                break;

            default:
                return false;
        }

        OnCurrencyChanged?.Invoke();
        return true;
    }
    public void UpdateUI()
    {
        silverText.text = playerSilver.ToString();
        goldText.text = playerGold.ToString();
    }
}
