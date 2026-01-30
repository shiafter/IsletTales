using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * currentSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);
    }
    public void SetMoveDirection(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
