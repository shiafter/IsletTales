using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int currenthealth;
    public int maxHealth;
    public int Health {  get { return currenthealth; } }

    public SpriteRenderer playerSprite;
    public HealthDisplay healthDisplay;

    private Animator animator;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(currenthealth <= 0)
        {
            currenthealth = maxHealth;
        }
        healthDisplay = FindObjectOfType<HealthDisplay>();
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHearts();
        }

        if (currenthealth <= 0)
        {
            playerSprite.enabled = false;
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        if(currenthealth >= maxHealth)
        {
            return;
        }
        int before = currenthealth;
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);

        Debug.Log($"Heal {amount} | {before} → {currenthealth}");

        if (healthDisplay == null)
        {
            healthDisplay = FindObjectOfType<HealthDisplay>();
        }
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
