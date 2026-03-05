using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ObjectHealth;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private Color color;

    [SerializeField] private int currentHealth;
    [SerializeField] private bool dead = false;
    private Vector3 spawnPosition;

    private EnemyMovement enemyMovement;
    public EnemyData enemyData;
    public BreakableObject breakable;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        breakable = GetComponent<BreakableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        enemyMovement = GetComponent<EnemyMovement>();

        if (spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }
        spawnPosition = transform.position;
    }
    private void Start()
    {
        ResetEnemy();
    }
    public void GetHit(int amount, GameObject sender)
    {
        if (dead) return;
        if (sender.layer == gameObject.layer) return;

        currentHealth -= amount;
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            dead = true;
            Defeated();
        }
    }
    public void Defeated()
    {
        StartCoroutine(DeathRoutine());
    }
    private void ResetEnemy()
    {
        dead = false;
        currentHealth = enemyData.maxHealth;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = spawnPosition;

        animator.SetBool("isDead", false);

        if (breakable != null)
        {
            breakable.ResetDrop();
        }
        if(enemyMovement != null)
        {
            enemyMovement.ResetState();
        }
    }
    public void RemoveEnemy()
    {
        if (enemyData.respawn)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = color;
    }
    private IEnumerator DeathRoutine()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;

        if (enemyMovement != null)
            enemyMovement.enabled = false;

        if (breakable != null)
            breakable.DestroyObject();

        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(0.75f);
        RemoveEnemy();
    }
    private IEnumerator Respawn()
    {
        if (enemyMovement != null) enemyMovement.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (collider != null) collider.enabled = false;

        yield return new WaitForSeconds(enemyData.respawnTime);

        ResetEnemy();
        rb.simulated = true;

        yield return new WaitForFixedUpdate();

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (collider != null) collider.enabled = true;
        if (enemyMovement != null) enemyMovement.enabled = true;
    }
}
