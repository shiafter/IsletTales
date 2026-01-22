using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject 
{
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemDescription;
    public int maxStack;
    public bool empty;
    public bool currency;

    public GameObject dropPrefab; 

    public enum ItemType
    {
        Equipment,
        Consumable,
        Material,
        Currency
    }
    public ItemType type;
}
