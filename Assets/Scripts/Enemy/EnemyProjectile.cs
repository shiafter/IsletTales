using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private int damage;

    private float knockbackForce;
    private float knockbackTime;

    public float lifeTime = 3f;
    public float speed = 6f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public void InitPrefab(int dmg, float kbForce, float kbTime)
    {
        damage = dmg;
        knockbackForce = kbForce;
        knockbackTime = kbTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                if (playerMovement != null)
                {
                    playerMovement.Knockback(transform, knockbackForce, knockbackTime);
                }

                Destroy(gameObject);
            }
            else if (!collision.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
