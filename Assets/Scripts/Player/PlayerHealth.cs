using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int health;
    public int maxHealth;
    public int Health {  get { return health; } }

    public SpriteRenderer playerSprite;
    public HealthDisplay healthDisplay;

    private Animator animator;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthDisplay = HealthDisplay.instance;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHearts();
        }

        if (health <= 0)
        {
            playerSprite.enabled = false;
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        if(health >= maxHealth)
        {
            return;
        }
        health += amount;

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHearts();
        }
    }
    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;

        if (healthDisplay != null)
        {
            healthDisplay.AddHearts();
            healthDisplay.UpdateHearts();
        }
            
    }


}
