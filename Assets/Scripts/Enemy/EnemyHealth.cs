using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ObjectHealth;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private bool dead = false;

    private SpriteRenderer spriteRenderer;
    private Color color;

    public EnemyData enemyData;
    public BreakableObject breakable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        breakable = GetComponent<BreakableObject>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }
    }
    private void Start()
    {
        InitializeHealth(enemyData.maxHealth);
    }
    public void InitializeHealth(int health)
    {
        currentHealth = health;
        dead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (dead) return;
        if (sender.layer == gameObject.layer) return;

        currentHealth -= amount;
        StartCoroutine(DamageFlash());
        if (currentHealth <= 0)
        {
            Defeated();
            if (breakable != null)
            {
                breakable.DestroyObject();
            }
        }
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = color;
    }
    public void Defeated()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("dead");
    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
