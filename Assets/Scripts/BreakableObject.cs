using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    public ItemData[] dropItems;

    public int minQuantity;
    public int maxQuantity;
    public void DestroyObject()
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            ItemData itemData = dropItems[i];

            int randomQuantity = Random.Range(minQuantity, maxQuantity + 1);
            if (randomQuantity <= 0) continue;

            Vector3 offset = Random.insideUnitCircle * 0.5f;

            GameObject itemDrop = Instantiate(
                itemData.dropPrefab,
                transform.position + offset,
                Quaternion.identity
            );

            Item item = itemDrop.GetComponent<Item>();

            // GÁN DATA
            item.item = itemData;
            item.quantity = randomQuantity;
        }
    }
}
