using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float knockbackForce;
    public float collisionCooldown;
    private float collisionTimer;

    public EnemyData enemyData;
    private void Update()
    {
        if(enemyData.enemyType == EnemyData.EnemyType.Collision)
        {
            collisionTimer -= Time.deltaTime;
        }
    }
    public void DealDamage(int damage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, enemyData.attackRange, enemyData.targetLayer);

        foreach (Collider2D hit in hits)
        {
            PlayerHealth playerHealth = hit.GetComponentInParent<PlayerHealth>();
            PlayerMovement playerMovement = hit.GetComponentInParent<PlayerMovement>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                playerMovement.Knockback(transform, knockbackForce, enemyData.knockbackTime);
                break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyData.enemyType != EnemyData.EnemyType.Collision) return;
        collisionTimer = 0f;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enemyData.enemyType != EnemyData.EnemyType.Collision) return;
        if (collisionTimer > 0) return;

        PlayerHealth playerHealth = collision.collider.GetComponentInParent<PlayerHealth>();
        PlayerMovement playerMovement = collision.collider.GetComponentInParent<PlayerMovement>();
        if (playerHealth == null) return;

        playerHealth.TakeDamage(enemyData.collisionDamage);
        playerMovement.Knockback(transform, knockbackForce, enemyData.knockbackTime);
        collisionTimer = collisionCooldown;

    }
    public void Attack()
    {
        if(enemyData.enemyType != EnemyData.EnemyType.Melee) return;
        DealDamage(enemyData.meeleDamage);
    }
    public void Explode()
    {
        if (enemyData.enemyType != EnemyData.EnemyType.Explode) return;

        DealDamage(enemyData.explodeDamage);
        Destroy(gameObject);
    }
    public void Shoot()
    {
        if (enemyData.enemyType != EnemyData.EnemyType.Range) return;
        //logic spawn prefab
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, enemyData.attackRange);
    }
}
