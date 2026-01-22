using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBookController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameController gameController;
    private bool isActive;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventoryPanel.SetActive(false);
        isActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            ToggleGuideBook();
        }
    }
    public void ToggleGuideBook()
    {
        isActive = !isActive;
        inventoryPanel.SetActive(isActive);
        Time.timeScale = isActive ? 0 : 1;

        gameController.IsUIBlockingInput = isActive; //ngăn cản click chuột khi tắt ui
    }
}
