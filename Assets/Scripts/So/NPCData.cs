using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite npcImage;
    [TextArea]
    public string shopDescription;
    public ShopData shopOwner;
}
