using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ItemData itemData;
    public TMP_Text itemNameText;
    public Image itemImage;

    private int price;

    public void Initialized(ItemData newItem)
    {
        itemData = newItem;
        itemImage.sprite = itemData.itemImage;
        itemNameText.text = itemData.itemName;
    }
    public void OnBuyButtonClick()
    {
        ShopManager.instance.TryBuyItem(itemData,price);
    }
}
