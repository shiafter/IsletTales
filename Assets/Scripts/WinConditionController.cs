using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionController : MonoBehaviour
{
    public static WinConditionController instance;
    [SerializeField] private WinCondition winCondition;
    [SerializeField] private GameObject portal;
    [SerializeField] private Transform spawnPosition;

    private bool portalSpawned = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public bool CheckWinCondition()
    {
        foreach (ItemData item in winCondition.requiredItems)
        {
            int amount = InventoryManager.instance.GetItemAmount(item);
            if (amount <= 0)
            {
                return false;
            }
        }
        return true;
    }
    public void TrySpawnPortal()
    {
        if (portalSpawned) return;

        if (CheckWinCondition())
        {
            Instantiate(portal, spawnPosition.position, Quaternion.identity);
            portalSpawned = true;

            Debug.Log("Portal spawned!");
        }
        else
        {
            Debug.Log("Chưa đủ vật phẩm");
        }
    }
}
