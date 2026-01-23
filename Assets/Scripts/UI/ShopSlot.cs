using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ShopItem shopItem;
    public TMP_Text itemNameText;
    public Image itemImage;

    private Button button;

    private int price;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Initialized(ShopItem newItem)
    {
        shopItem = newItem;
        itemImage.sprite = shopItem.itemData.itemImage;
        itemNameText.text = shopItem.itemData.itemName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        ShopManager.instance.ShowItemInfo(shopItem);
    }
    public void OnBuyButtonClick()
    {

    }
}
