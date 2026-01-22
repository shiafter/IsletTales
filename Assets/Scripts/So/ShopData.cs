using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class ShopData : ScriptableObject 
{
    public List<ShopItem> items;
    public NPCData npcData;
}
[System.Serializable]
public class ItemPrice
{
    public CurrencyData currencyData;
    public int amount;
}
[System.Serializable]
public class ShopItem
{
    public ItemData itemData;
    public List<ItemPrice> price;
}