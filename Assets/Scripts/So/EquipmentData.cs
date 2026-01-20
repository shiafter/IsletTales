using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu]
public class EquipmentData : ItemData
{
    public int damage;
    public float range;
    public float actionDelay = 0.3f;
    public Sprite equipSprite;
    public SpriteLibraryAsset spriteAsset;
    public enum EquipmentType
    {
        None,
        Sword,
        Staff,
        Axe,
        Pickaxe
    }
    public enum Material
    {
        None,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    public Material material;

    public EquipmentType equipType;
}
