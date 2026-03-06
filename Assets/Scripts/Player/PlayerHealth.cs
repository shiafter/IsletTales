using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public int currenthealth;
    public int maxHealth;
    public bool dead {  get; private set; }
    public int Health {  get { return currenthealth; } }
    private Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    public HealthDisplay healthDisplay;

    public Animator playerAnimator;
    public static PlayerHealth instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(currenthealth <= 0)
        {
            currenthealth = maxHealth;
        }
        healthDisplay = FindObjectOfType<HealthDisplay>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage received: " + damage);
        if (dead) return;

        currenthealth -= damage;
        SoundEffectManager.Play("Hurt");

        if (healthDisplay != null)
        {
            healthDisplay.UpdateHearts();
        }

        if (currenthealth <= 0)
        {
            if(dead) return;
            dead = true;
            SoundEffectManager.Play("GameOver");

            rb.velocity = Vector2.zero;
            rb.simulated = false;

            playerAnimator.ResetTrigger("Hurt");
            playerAnimator.SetTrigger("dead");
            DisablePlayerControl();
        }
        else
        {
            playerAnimator.SetTrigger("Hurt");
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
        currenthealth += amount;
        SoundEffectManager.Play("IncreaseHealth");

        if (healthDisplay != null)
        {
            healthDisplay.AddHearts();
            healthDisplay.UpdateHearts();
        }
            
    }
    private void DisablePlayerControl()
    {
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }
    }
}
