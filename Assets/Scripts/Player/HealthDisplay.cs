using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public static HealthDisplay instance;

    public GameObject heart;
    private List<Image> hearts = new List<Image>();

    public PlayerHealth playerHealth;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        playerHealth = PlayerHealth.instance;

        for(int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject h = Instantiate(heart, this.transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }

    public void UpdateHearts()
    {
        int heartFill = playerHealth.Health;

        foreach(Image i in hearts)
        {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }
    }

    public void AddHearts()
    {
        foreach(Image i in hearts)
        {
            Destroy(i.gameObject);
        }
        hearts.Clear();

        for(int i = 0; i< playerHealth.maxHealth; i++)
        {
            GameObject h = Instantiate(heart, this.transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }
}
