using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;
    [SerializeField] private int startingGold = 10;
    private int playerGold;
    public event Action<int> OnGoldChanged;
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
        }
    }
    public int GetGold() => playerGold;
    public bool SpendGold(int amount)
    {
        if(playerGold >= amount)
        {
            playerGold -= amount;
            OnGoldChanged?.Invoke(playerGold);
            return true;
        }
        return false;
    }
    public void AddGold(int amount)
    {
        playerGold += amount;
        OnGoldChanged?.Invoke(playerGold);
    }
}
