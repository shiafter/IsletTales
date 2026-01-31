using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class EnemyMovement : MonoBehaviour
{
    private int facingDirection = 1;
    private float cooldownTimer;
    public Transform attackPoint;
    public Transform detectionPoint;

    //component
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    //enemy data
    public EnemyData enemyData;
    private EnemyState enemyState;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }
    void Update()
    {
        if (enemyState == EnemyState.Attack)
            return;
        CheckForPlayer();
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if(enemyState == EnemyState.Chase)
        {
            Chase();
        }else if(enemyState == EnemyState.Attack)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void Chase()
    {
        if (player.position.x < transform.position.x && facingDirection == 1 || player.position.x > transform.position.x && facingDirection == -1)
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * enemyData.moveSpeed;
    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, enemyData.chaseRange, enemyData.targetLayer);
        if(hits.Length > 0)
        {
            player = hits[0].transform;

            if (enemyData.enemyType == EnemyData.EnemyType.Collision)
            {
                ChangeState(EnemyState.Chase);
                return;
            }

            if (Vector2.Distance(attackPoint.position, player.transform.position) <= enemyData.attackRange && cooldownTimer <= 0)
            {
                cooldownTimer = enemyData.attackCooldown;
                rb.velocity = Vector2.zero;
                ChangeState(EnemyState.Attack);
            }else if(Vector2.Distance(transform.position, player.transform.position) > enemyData.attackRange && enemyState != EnemyState.Attack)
            {
                ChangeState(EnemyState.Chase);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }
    private void ChangeState(EnemyState newState)
    {
        //rời trạng thái cũ 
        if(enemyState == EnemyState.Idle)
        {
            animator.SetBool("idle", false);
        }else if(enemyState == EnemyState.Chase)
        {
            animator.SetBool("walk", false);
        }
        else if (enemyState == EnemyState.Attack)
        {
            animator.SetBool("attack", false);
        }
        //lưu trạng thái mới 
        enemyState = newState;
        //update animation
        if (enemyState == EnemyState.Idle)
        {
            animator.SetBool("idle", true);
        }
        else if (enemyState == EnemyState.Chase)
        {
            animator.SetBool("walk", true);
        }
        else if (enemyState == EnemyState.Attack)
        {
            animator.SetBool("attack", true);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, enemyData.chaseRange);
    }
}
public enum EnemyState
{
    Idle,
    Chase,
    Attack
}
