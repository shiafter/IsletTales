using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //[SerializeField]
    //public string itemName;

    [SerializeField]
    public int quantity;

    //[SerializeField]
    //public Sprite sprite;

    public ItemData item;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Panel").GetComponentInChildren<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int leftOverItem = inventoryManager.AddItem(item, quantity);
            int pickedAmount = quantity - leftOverItem;

            if(pickedAmount > 0)
            {
                PickupItemNoti.Instance?.ShowItemPopup(item.itemName, pickedAmount);
            }
            if(leftOverItem <= 0)
            {
                Destroy(gameObject);
            }else
            {
                quantity = leftOverItem;
            }
            
        }
    }
}
