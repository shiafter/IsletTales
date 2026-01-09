using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField]
    public int currentHealth, maxHealth;
    [SerializeField]
    private bool dead = false;

    private SpriteRenderer spriteRenderer;
    private Color color;
    [SerializeField]
    private Color hitFlash;
    BreakableObject breakable;

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

        if(spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }
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
            if (breakable != null)
            {
                breakable.DestroyObject();
            }
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = hitFlash;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = color;
    }
}
