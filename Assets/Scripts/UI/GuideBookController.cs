using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBookController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject tutorialPanel;
    public GameController gameController;
    private bool isInventoryActive;
    private bool isTutorialActive;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventoryPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        isInventoryActive = false;
        isTutorialActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            ToggleInventory();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            ToggleGuideBook();
        }
    }
    public void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        inventoryPanel.SetActive(isInventoryActive);
        Time.timeScale = isInventoryActive ? 0 : 1;

        gameController.UIBlockingInput = isInventoryActive; //ngăn cản click chuột khi tắt ui
    }
    public void ToggleGuideBook()
    {
        isTutorialActive = !isTutorialActive;
        tutorialPanel.SetActive(isTutorialActive);
        Time.timeScale = isTutorialActive ? 0 : 1;

        gameController.UIBlockingInput = isTutorialActive; //ngăn cản click chuột khi tắt ui
    }
}
