using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static ItemData;
using static Unity.VisualScripting.Member;

public class HotbarSlot : MonoBehaviour,IDropHandler, IBeginDragHandler
{
    public Image itemIcon;
    public GameObject selectedFrame;
    public int numbeOfSlot = 9;

    public ItemData itemData;
    public int quantity;
    public TMP_Text quantityText;
    public bool empty => itemData == null || itemData.empty;

    public void SetItem(ItemData item, int qty)
    {
        itemData = item;
        quantity = qty;

        itemIcon.sprite = itemData.itemImage;
        itemIcon.enabled = true;
        quantityText.text = quantity.ToString();
    }
    public void Clear()
    {
        itemData = null;
        quantity = 0;
        itemIcon.enabled = false;
    }
    public void SelectSlot()
    {
        selectedFrame.SetActive(true);
        if (itemData == null) return;
        if(itemData.type == ItemData.ItemType.Equipment)
        {

        }
    }
    public void DeselectSlot()
    {
        selectedFrame.SetActive(false);
    }
    public void UseItem()
    {
        if(itemData == null) return;

        if (itemData.type == ItemType.Consumable)
        {
            InventoryManager.instance.UseItem(itemData.itemName);
            quantity--;

            if (quantity <= 0)
                Clear();
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var sourceItem = DragManager.instance.sourceSlot;

        if (sourceItem is ItemSlot inventorySlot)
        {
            SwapWithInventory(inventorySlot);
            return;
        }
    }

    private void SwapWithInventory(ItemSlot inventorySlot)
    {
        ItemData tempItem = itemData;
        int tempQty = quantity;

        SetItem(inventorySlot.itemData, inventorySlot.quantity);

        inventorySlot.itemData = tempItem;
        inventorySlot.quantity = tempQty;
        inventorySlot.UpdateUI();
    }

    private void SwapWithHotbar(HotbarSlot hotbarSlot)
    {
        if (hotbarSlot == this) return;

        ItemData tempItem = itemData;
        int tempQty = quantity;

        SetItem(hotbarSlot.itemData, hotbarSlot.quantity);
        hotbarSlot.SetItem(tempItem, tempQty);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty) return;

        //DragManager.instance.EnableDrag(itemIcon.sprite, this);
    }
}
