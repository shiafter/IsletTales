using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Panel").GetComponentInChildren<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int leftOverItem = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
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
