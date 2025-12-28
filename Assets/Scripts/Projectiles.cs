using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectiles : MonoBehaviour
{
    Rigidbody2D projectileRb;
    public float speed;
    public float projectileLifetime;
    public float countdown;
    // Start is called before the first frame update
    void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        countdown = projectileLifetime;
    }

    // Update is called once per frame
    private void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //projectileRb.velocity = new Vector2(speed, projectileRb.velocity.y);
    }
    public void Fire(Vector2 direction, float force)
    {
        projectileRb.AddForce(direction.normalized * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);

            // Example knockback effect if enemy has Rigidbody2D
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDir * 3f);
            }
        }
        Destroy(gameObject);
    }
}
