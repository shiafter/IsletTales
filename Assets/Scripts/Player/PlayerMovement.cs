using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float maxSpeed = 3, acceleration = 50, deacceleration = 100;
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    private bool knockback;
    public Vector2 movementInput { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback == false)
        {
            if (movementInput.magnitude > 0 && currentSpeed >= 0)
            {
                oldMovementInput = movementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb.velocity = oldMovementInput * currentSpeed;
        }
    }
    public void Knockback(Transform enemy, float force, float knockbackTime)
    {
        knockback = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(knockbackTime));
    }
    IEnumerator KnockbackCounter(float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        knockback = false;
    }
}
