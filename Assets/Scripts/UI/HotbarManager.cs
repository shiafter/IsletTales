using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance;

    public ItemSlot[] slots;
    public int selectedIndex = -1;

    public Equipment equipment;
    private InventoryManager inventoryManager;

    public ItemData[] startingItem;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventoryManager = InventoryManager.instance;

        if (inventoryManager == null)
            Debug.LogError("InventoryManager.instance == null");
        InitStartingItem();
        SelectSlot(0);
    }
    private void Update()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(Keyboard.current[(Key)((int)Key.Digit1 + i)]?.wasPressedThisFrame == true)
            {
                SelectSlot(i);
            }
        }
    }
    public void InitStartingItem()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i >= startingItem.Length)
            {
                break;
            }
            ItemData item = startingItem[i];
            if(item == null || item.empty)
            {
                continue;
            }
            slots[i].AddItem(item, 1);
        }
    }
    public void SelectSlot(int index)
    {
        if(index < 0 || index >= slots.Length) return; //vượt quá số lượng slot

        if (slots[index] == null || slots[index].itemData == null || slots[index].itemData.empty) return; //slot trống thì bỏ qua 

        if (selectedIndex == index)
        {
            if (slots[index].itemData.type == ItemData.ItemType.Consumable)
            {
                inventoryManager.UseItem(slots[index].itemData.itemName);
                slots[index].quantity--;
                if (slots[index].quantity <= 0)
                {
                    slots[index].EmptySlot();
                }
                else
                {
                    slots[index].UpdateUI();
                }
            }
            return;
        }

        if(selectedIndex >= 0)
        {
            slots[selectedIndex].selectedFrame.SetActive(false);
        }
        selectedIndex = index;
        slots[index].selectedFrame.SetActive(true);

        if (slots[index].itemData.type == ItemData.ItemType.Equipment)
        {
            EquipmentData equipmentData = slots[index].itemData as EquipmentData;
            if (equipmentData == null)
            {
                Debug.LogWarning("Item chua ep kieu thanh Equipment");
                return;
            }
            equipment.Equip(equipmentData);
        }
    }
}
