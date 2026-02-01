using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    //public float respawnTime;
    [SerializeField]
    public DropItem[] dropItems;

    public void DestroyObject()
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            DropItem drop = dropItems[i];

            int randomQuantity = Random.Range(drop.minQuantity, drop.maxQuantity + 1);
            if (randomQuantity <= 0) continue;

            Vector3 offset = Random.insideUnitCircle * 0.5f;

            GameObject itemDrop = Instantiate(
                drop.itemData.dropPrefab,
                transform.position + offset,
                Quaternion.identity
            );

            Item item = itemDrop.GetComponent<Item>();

            // GÁN DATA
            item.item = drop.itemData;
            item.quantity = randomQuantity;
        }
    }
}
[System.Serializable]
public class DropItem
{
    public ItemData itemData;
    public int minQuantity;
    public int maxQuantity;
}