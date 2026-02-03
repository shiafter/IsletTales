using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCData npcData;

    public bool CanInteract()
    {
        return true;
    }
    public void Interact()
    {
        if (ShopManager.instance == null) return;
        ShopManager.instance.ToggleShop(npcData.shopOwner);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerInteract>()?.SetInteract(this);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerInteract>()?.ClearInteract(this);
        }
    }
}
