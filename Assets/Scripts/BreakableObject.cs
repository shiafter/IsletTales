using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    public ItemData[] dropItems;

    public int minQuantity = 1;
    public int maxQuantity = 3;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth >= 0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            ItemData itemData = dropItems[i];

            int randomQuantity = Random.Range(minQuantity, maxQuantity + 1);
            if (randomQuantity <= 0) continue;

            Vector3 offset = new Vector3(i * 0.5f, 0, 0);

            GameObject itemDrop = Instantiate(
                itemData.dropPrefab,
                transform.position + offset,
                Quaternion.identity
            );

            Item item = itemDrop.GetComponent<Item>();

            // GÁN DATA
            item.item = itemData;
            item.quantity = randomQuantity;

            // CẬP NHẬT HÌNH ẢNH
            SpriteRenderer sr = itemDrop.GetComponent<SpriteRenderer>();
            sr.sprite = itemData.itemImage;
        }

        Destroy(gameObject);
    }
}
