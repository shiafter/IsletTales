using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlot[] itemSlot;
    public ItemScriptableObject[] itemSO;
    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItem = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if(leftOverItem > 0)
                {
                    leftOverItem = AddItem(itemName, leftOverItem, sprite, itemDescription);
                }
                return leftOverItem;
            }
        }
        return quantity;
    }

    public void DeselectAllSlot()
    {
        for(int i = 0; i < itemSlot.Length; ++i)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    public void UseItem(string itemName)
    {
        for(int i = 0; i < itemSO.Length; i++)
        {
            if(itemSO[i].itemName == itemName)
            {
                itemSO[i].UseItem();
            }
        }
    }
}
