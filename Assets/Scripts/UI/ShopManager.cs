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
    private ShopItem selectedItem;
    private Dictionary<ItemData, int> boughtCount = new();

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
    [SerializeField] private TMP_Text inStockText;

    //===BUY BUTTON===
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buyText;
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

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyButtonClicked);
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

        foreach (var item in currentShop.items)
        {
            if (!boughtCount.ContainsKey(item.itemData))
            {
                boughtCount.Add(item.itemData, 0);
            }
        }
        shopPanel.SetActive(true);
        ShowShopInfo();
        SetShopItem();

        if (currentShop.items.Count > 0)
        {
            ShowItemInfo(currentShop.items[0]);
        }

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

        selectedItem = item;
        Debug.Log("SHOW ITEM INFO: " + item.itemData.itemName);

        //item data
        itemIcon.sprite = item.itemData.itemImage;
        itemName.text = item.itemData.itemName;
        itemDescription.text = item.itemData.itemDescription;

        for(int i = pricePanel.childCount - 1; i >= 0; i--)
        {
            Destroy(pricePanel.GetChild(i).gameObject);
        }
        //price ui
        foreach(ItemPrice itemPrice in item.price)
        {
            if (itemPrice.amount <= 0) continue;
            GameObject price = Instantiate(pricePrefab, pricePanel);

            Image currencyIcon = price.transform.Find("Currency Icon").GetComponent<Image>();
            TMP_Text priceAmountText = price.transform.Find("Amount Text").GetComponent<TMP_Text>();

            currencyIcon.sprite = itemPrice.currencyData.itemImage;
            priceAmountText.text = itemPrice.amount.ToString();

        }
        UpdateInStock();
        CheckBuyButton();
    }
    private int GetBoughtCount(ShopItem item)
    {
        if(item == null || item.itemData == null)
        {
            return 0;
        }
        return boughtCount.TryGetValue(item.itemData, out int count) ? count : 0;
    }
    private void UpdateInStock()
    {
        if (selectedItem == null)
        {
            inStockText.text = "";
            return;
        }
        int bought = GetBoughtCount(selectedItem);
        int remain = Mathf.Max(0, selectedItem.inStock - bought);
        
        if (selectedItem.inStock < 0 || remain <= 0)
        {
            inStockText.text = "Sold Out";
        }
        else
        {
            inStockText.text = $"Instock: {remain}/{selectedItem.inStock}";
        }
    }
    private bool ReachLimitBoughtCount(ShopItem item)
    {
        if (item.inStock < 0) return true;
        int bought = GetBoughtCount(item);
        return bought >= item.inStock;
    }
    public bool CanBuyItem(ShopItem item)
    {
        if(item ==  null || item.itemData == null) return false;

        if (ReachLimitBoughtCount(item)) return false;

        if(!InventoryManager.instance.HasSpaceForItem(item.itemData, 1)) return false;

        foreach (ItemPrice price in item.price)
        {
            if (price == null || price.currencyData == null || price.amount <= 0) continue; //giá mua k yêu cầu thì không cần check 

            var type = price.currencyData.currencyType;
            switch (type)
            {
                case CurrencyData.CurrencyType.Silver:
                    if (CurrencyController.instance.GetSilver() < price.amount)
                        return false;
                    break;
                case CurrencyData.CurrencyType.Gold:
                    if (CurrencyController.instance.GetGold() < price.amount)
                        return false;
                    break;
                case CurrencyData.CurrencyType.Ores:
                    int owned = InventoryManager.instance.GetItemAmount(price.currencyData);
                    Debug.Log($"CHECK ORE: {price.currencyData.name} | Owned = {owned}");
                    if (owned < price.amount)
                        return false;
                    break;

            }
        }
        return true;
    }
    public bool BuyItem()
    {
        if (!CanBuyItem(selectedItem))  return false;

        InventoryManager.instance.AddItem(selectedItem.itemData, 1);

        foreach (ItemPrice price in selectedItem.price)
        {
            if (price == null  || price.currencyData == null || price.amount <=0) continue;

            var type = price.currencyData.currencyType;
            if (type == CurrencyData.CurrencyType.Gold || type == CurrencyData.CurrencyType.Silver) // nếu mua bằng gold và sil thì trừ đi trong currency controller
            {
                CurrencyController.instance.SpendCurrency(type, price.amount);
            }else if(type == CurrencyData.CurrencyType.Ores) // nếu phải mua bằng cả ore thì trừ đi 1 trong tổng số lượng trong túi
            {
                InventoryManager.instance.RemoveItem(price.currencyData, price.amount);
            }
        }
        boughtCount[selectedItem.itemData]++;
        return true;
    }
    private void CheckBuyButton()
    {
        if (selectedItem == null)
        {
            buyButton.interactable = false;
            return;
        }
        if (ReachLimitBoughtCount(selectedItem))
        {
            buyText.text = "Sold Out";
            buyButton.interactable = false;
            return;
        }

        bool canBuy = CanBuyItem(selectedItem);
        buyButton.interactable = canBuy;
        buyText.text = canBuy ? "Buy" : "Not enough money";
    }
    private void OnBuyButtonClicked()
    {
        if (selectedItem == null)
            return;

        bool success = BuyItem();

        if (success)
        {
            SoundEffectManager.Play("PickUpItem");
            ShowItemInfo(selectedItem);
        }
    }
}