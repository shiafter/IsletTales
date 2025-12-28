using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //====ITEM DATA====
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    [SerializeField]
    private int maxNumberOfItem;

    //====ITEM SLOT====
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    //====ITEM INFO FIELD====
    public Image itemInfoImage;
    public TMP_Text itemInfoName;
    public TMP_Text itemInfoDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;
    public Sprite emptySprite;

    private float lastClickTime;
    private float doubleClickTime = 0.3f;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Panel").GetComponentInChildren<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //check if slot is filled
        if (isFull)
        {
            return quantity;
        }
        //update item in slot's info
        this.itemName = itemName;

        this.itemSprite = itemSprite;
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

        this.itemDescription = itemDescription;

        //if item is already in inventory, add number of quantity
        this.quantity += quantity;
        if(this.quantity >= maxNumberOfItem)
        {
            quantityText.text = this.quantity.ToString();
            quantityText.enabled = true;
            isFull = true;
            
            //one slot can contain limit number of item
            int extraItem = this.quantity - maxNumberOfItem;
            this.quantity = extraItem;
            return extraItem;
        }

        //if quantity not exceed max number of item
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            // Kiểm tra double click
            if (Time.time - lastClickTime <= doubleClickTime)
            {
                // Double click -> sử dụng item
                inventoryManager.UseItem(itemName);

                this.quantity -= 1;
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
                else
                {
                    quantityText.text = this.quantity.ToString();
                } 
                lastClickTime = 0f;
                return;
            }
            lastClickTime = Time.time;
        }
        else
        {
            inventoryManager.DeselectAllSlot();

            selectedShader.SetActive(true);
            thisItemSelected = true;

            itemInfoImage.sprite = itemSprite;
            itemInfoName.text = itemName;
            itemInfoDescriptionText.text = itemDescription;
            if (itemInfoImage.sprite == null)
            {
                itemInfoImage.sprite = emptySprite;
            }
        }
    }

    private  void EmptySlot()
    {
        quantity = 0;
        quantityText.enabled = false;

        itemName = "";
        itemSprite = null;

        itemImage.enabled = false;

        itemInfoImage.sprite = emptySprite;
        itemInfoImage.color = Color.white;
        itemInfoName.text = "";
        itemInfoDescriptionText.text = "";

        isFull = false;
        thisItemSelected = false;
        selectedShader.SetActive(false);
    }

    public void OnRightClick()
    {
        GameObject itemDrop = new GameObject(itemName);
        Item newItem = itemDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        SpriteRenderer sr = itemDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        itemDrop.AddComponent<BoxCollider2D>();

        itemDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 0, 0);

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if(this.quantity <= 0)
        {
            EmptySlot();
        }

    }
}
