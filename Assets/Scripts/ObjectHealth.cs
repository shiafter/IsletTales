using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;
    private bool dead = false;
    private Vector3 spawnPosition;
    public float respawnTime;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private Color color;
    private BreakableObject breakable;
    public enum RequiredTool
    {
        None,
        Axe,
        Pickaxe
    }
    public RequiredTool requiredTool;
    private void Awake()
    {
        breakable = GetComponent<BreakableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        if(spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }
        spawnPosition = transform.position;
        currentHealth = maxHealth;
    }
    public void GetHit(int amount, GameObject sender, EquipmentData currentEquip)
    {
        if (dead) return;
        if (sender.layer == gameObject.layer) return;
        if (currentEquip == null || currentEquip.equipType.ToString() != requiredTool.ToString()) return;

        currentHealth -= amount;
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            dead = true;
            StartCoroutine(Respawn());
        }
    }
    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = color;
    }
    private IEnumerator Respawn()
    {
        if(breakable != null)
        {
            breakable.DestroyObject();
        }
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if(collider != null) collider.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        transform.position = spawnPosition;
        currentHealth = maxHealth;
        dead = false;

        if(breakable != null)
        {
            breakable.ResetDrop();
        }

        if(spriteRenderer != null) spriteRenderer.enabled = true;
        if(collider != null) collider.enabled = true;
    }
}
