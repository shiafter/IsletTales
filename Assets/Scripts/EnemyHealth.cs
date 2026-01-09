using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ObjectHealth;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private int currentHealth, maxHealth;

    [SerializeField]
    private bool dead = false;

    private SpriteRenderer spriteRenderer;
    private Color color;
    //[SerializeField]
    //private Color hitFlash;
    BreakableObject breakable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        breakable = GetComponent<BreakableObject>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }
    }
    public void InitializeHealth(int health)
    {
        currentHealth = health;
        maxHealth = health;
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
            animator.SetTrigger("dead");
            if (breakable != null)
            {
                breakable.DestroyObject();
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = color;
    }
}
