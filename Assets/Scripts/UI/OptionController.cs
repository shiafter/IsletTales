using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Button[] optionButtons;
    public GameObject[] pages;

    public Sprite normalSprite;
    public Sprite activeSprite;


    private void Start()
    {
        ActivateOption(0);
    }
    public void ActivateOption(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            optionButtons[i].image.sprite = normalSprite;
        }
        pages[index].SetActive(true);
        optionButtons[index].image.sprite = activeSprite;


    }
}
