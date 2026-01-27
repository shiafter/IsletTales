using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public ItemSlot[] itemSlot;
    public ConsumableItem[] consumableItem;

    //====ITEM INFO FIELD====
    public Image itemInfoImage;
    public TMP_Text itemInfoName;
    public TMP_Text itemInfoDescriptionText;
    public Sprite emptySprite;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public int AddItem(ItemData item, int amount) 
    {

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemData != null && itemSlot[i].itemData == item && !itemSlot[i].full) //nếu slot đã có cùng item và chưa full thì thêm số lượng
            {
                amount = itemSlot[i].AddItem(item, amount);
                if(amount <= 0)
                {
                    return 0;
                }
            }
        }
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemData == null || itemSlot[i].itemData.empty) //nếu slot trống thì thêm item vào slot
            {
                amount = itemSlot[i].AddItem(item, amount);
                if (amount <= 0)
                {
                    return 0;
                }
            }
        }
        return amount;
    }

    public void UpdateItemInfo(ItemData item)
    {
        if(item == null)
        {
            itemInfoImage.sprite = emptySprite;
            itemInfoName.text = "";
            itemInfoDescriptionText.text = "";
            return;
        }

        itemInfoImage.sprite = item.itemImage;
        itemInfoName.text = item.itemName;
        itemInfoDescriptionText.text = item.itemDescription;
    }

    public void DeselectAllSlot()
    {
        for(int i = 0; i < itemSlot.Length; ++i)
        {
            itemSlot[i].selectedFrame.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
    public void UseItem(string itemName)
    {
        for(int i = 0; i < consumableItem.Length; i++)
        {
            if(consumableItem[i].itemName == itemName)
            {
                consumableItem[i].UseItem();
            }
        }
    }
    public int GetItemAmount(ItemData item) //lấy tổng số lượng item người chơi có (cộng từ tất cả các slot)
    {
        int totalAmount = 0;
        foreach (var slot in itemSlot)
        {
            if(slot.itemData == item)
            {
                totalAmount += slot.quantity;
            }
        }
        return totalAmount;
    }
    public bool RemoveItem(ItemData item, int amount) //kiểm tra xem item có bị xóa đi sau khi trừ 1 số lượng nhất định không
    {
        for(int i = 0; i  < itemSlot.Length; i++)
        {
            if(itemSlot[i].itemData == item)
            {
                amount = itemSlot[i].RemoveAmount(amount);
                if(amount <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool HasSpaceForItem(ItemData item, int amount)
    {
        int remaining = amount;
        // check stack
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.itemData == item && !slot.full)
            {
                int space = item.maxStack - slot.quantity;
                remaining -= space;
                if (remaining <= 0)
                    return true;
            }
        }
        // check slot trống
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.itemData == null || slot.itemData.empty)
            {
                remaining -= item.maxStack;
                if (remaining <= 0)
                    return true;
            }
        }
        return false;
    }

}
