using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    public GameObject[] header;
    public Sprite[] normalSprite;
    public Sprite[] activeSprite;
    

    private void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab (int index)
    {
        for(int i = 0; i <pages.Length; i++)
        {
            pages[i].SetActive(false);
            header[i].SetActive(false);
            tabImages[i].sprite = normalSprite[i];
        }
        pages[index].SetActive(true);
        tabImages[index].sprite = activeSprite[index];
        header[index].SetActive(true);
        
    }
}
