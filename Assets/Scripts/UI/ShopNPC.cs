using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCData npcData;
    private void Start()
    {
        
    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (ShopManager.instance == null) return;
        ShopManager.instance.OpenShop(npcData.shopOwner);
    }
}
