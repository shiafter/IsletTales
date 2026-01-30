using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float knockbackForce;

    public EnemyData enemyData;
    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, enemyData.attackRange, enemyData.targetLayer);

        foreach (Collider2D hit in hits)
        {
            PlayerHealth playerHealth = hit.GetComponentInParent<PlayerHealth>();
            PlayerMovement playerMovement = hit.GetComponentInParent<PlayerMovement>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyData.attackDamage);
                playerMovement.Knockback(transform, knockbackForce, enemyData.knockbackTime);
                break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, enemyData.attackRange);
    }
}
