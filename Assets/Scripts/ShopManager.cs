using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    [SerializeField]private ShopData currentShop;

    [SerializeField] private ShopSlot[] shopSlots;

    public GameObject shopPanel;
    //===SHOP INFORMATION===
    [SerializeField] private Image NPCImage;
    [SerializeField] private TMP_Text NPCName;
    [SerializeField] private TMP_Text shopDescription;

    //===ITEM INFORMATION===
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        shopPanel.SetActive(false);
    }
    public void OpenShop(ShopData shopData)
    {
        currentShop = shopData;
        ShowShopInfo();
        SetShopItem();
    }
    public void SetShopItem()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            if(i < currentShop.items.Count)
            {
                ShopItem shopItem = currentShop.items[i];
                //shopSlots[i].Initialized(shopItem.itemData, shopItem.price); //fill item vào trong các shop slot
                shopSlots[i].gameObject.SetActive(true);
            }
            shopSlots[i].gameObject.SetActive(false); //các ô chưa fill item thì tắt đi 
        }
            
    }
    public void ShowShopInfo()
    {
        NPCImage.sprite = currentShop.npcData.npcImage;
        NPCName.text = currentShop.npcData.npcName;
        shopDescription.text = currentShop.npcData.shopDescription;
    }
    public void ShowItemInfo()
    {

    }
    public void TryBuyItem(ItemData  item, int price)
    {
        if(item != null)
        {

        }
    }
}


