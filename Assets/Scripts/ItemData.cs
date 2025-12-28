using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject 
{
    public string itemName;
    public Sprite sprite;
    [TextArea] public string itemDescription;
    public GameObject dropPrefab; 
}
