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

    public bool isOpening => shopPanel.activeSelf;
    private GameController gameController;

    public GameObject shopPanel;
    //===SHOP INFORMATION===
    [SerializeField] private TMP_Text shopTitle;
    [SerializeField] private Image NPCImage;
    [SerializeField] private TMP_Text NPCName;
    [SerializeField] private TMP_Text shopDescription;

    //===ITEM INFORMATION===
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    //===ITEM PRICE===
    [SerializeField] private Transform pricePanel;
    [SerializeField] private GameObject pricePrefab;
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
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        shopPanel.SetActive(false);
    }
    public void ToggleShop(ShopData shop)
    {
        if (isOpening)
        {
            CloseShop();
        }else if(shop != null)
        {
            OpenShop(shop);
        }
    }
    public void OpenShop(ShopData shopData)
    {
        currentShop = shopData;
        shopPanel.SetActive(true);
        ShowShopInfo();
        SetShopItem();

        gameController.IsUIBlockingInput = true;
        Time.timeScale = 0f;
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        currentShop = null;

        gameController.IsUIBlockingInput = false;
        Time.timeScale = 1f;
    }
    public void SetShopItem()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            if(i < currentShop.items.Count)
            {
                var shopItem = currentShop.items[i];
                shopSlots[i].Initialized(shopItem); //fill item vào trong các shop slot
                shopSlots[i].gameObject.SetActive(true);
            }
            else
            {
                shopSlots[i].gameObject.SetActive(false); //các ô chưa fill item thì tắt đi
            }
            
        }
    }
    public void ShowShopInfo()
    {
        shopTitle.text = currentShop.shopName;
        NPCImage.sprite = currentShop.npcData.npcImage;
        NPCName.text = currentShop.npcData.npcName;
        shopDescription.text = currentShop.npcData.shopDescription;
    }
    public void ShowItemInfo(ShopItem item)
    {
        if (item == null || item.itemData == null) return;

        //item data
        itemIcon.sprite = item.itemData.itemImage;
        itemName.text = item.itemData.itemName;
        itemDescription.text = item.itemData.itemDescription;
        for(int i = pricePanel.childCount - 1; i >= 0; i--)
        {
            Destroy(pricePanel.GetChild(i).gameObject);
        }

        foreach(ItemPrice itemPrice in item.price)
        {
            if (itemPrice.amount <= 0) continue;
            GameObject price = Instantiate(pricePrefab, pricePanel);

            Image currencyIcon = price.transform.Find("Currency Icon").GetComponent<Image>();
            TMP_Text priceAmountText = price.transform.Find("Amount Text").GetComponent<TMP_Text>();

            currencyIcon.sprite = itemPrice.currencyData.icon;
            priceAmountText.text = itemPrice.amount.ToString();

        }
    }
}


