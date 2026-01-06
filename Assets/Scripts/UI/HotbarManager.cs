using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager instance;

    public ItemSlot[] slots;
    public int selectedIndex = -1;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventoryManager = InventoryManager.instance;

        if (inventoryManager == null)
            Debug.LogError("InventoryManager.instance == null");
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

    public void SelectSlot(int index)
    {
        if(index < 0 || index >= slots.Length)
        {
            return;
        }

        if (selectedIndex == index)
        {
            if (slots[index].itemData == null || slots[index].itemData.empty)
                return;

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
            return;
        }

        if(selectedIndex >= 0)
        {
            slots[selectedIndex].selectedFrame.SetActive(false); //tắt selectframe của slot trước đó 
        }

        selectedIndex = index;
        slots[index].selectedFrame.SetActive(true);
    }
}
