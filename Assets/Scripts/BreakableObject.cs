using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] public DropItem[] dropItems;
    private bool dropped;
    public void DestroyObject()
    {
        if (dropped) return;
        dropped = true;

        for (int i = 0; i < dropItems.Length; i++)
        {
            DropItem drop = dropItems[i];

            int randomQuantity = Random.Range(drop.minQuantity, drop.maxQuantity + 1);
            if (randomQuantity <= 0) continue;

            Vector3 offset = Random.insideUnitCircle * 0.5f;

            GameObject itemDrop = Instantiate(drop.itemData.dropPrefab, transform.position + offset, Quaternion.identity);
            
            // GÁN DATA
            Item item = itemDrop.GetComponent<Item>();
            item.item = drop.itemData;
            item.quantity = randomQuantity;
        }
    }
    public void ResetDrop()
    {
        dropped = false;
    }
}
[System.Serializable]
public class DropItem
{
    public ItemData itemData;
    public int minQuantity;
    public int maxQuantity;
}