using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition {  get; set; }
    private Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    private Health health;

    public bool isAttacking {  get; private set; }
    public Transform circleOrigin;
    public float radius;

    private void Awake()
    {
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();


    }
    private void Update()
    {
        if (isAttacking)
        {
            return;
        }

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        //transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            weaponRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            weaponRenderer.flipX = false;
        }
        transform.localScale = scale;
    }

    public void Attack()
    {
        if (attackBlocked)
        {
            return;
        }
        isAttacking = true;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    
    
    public void ResetIsAttacking()
    {
        isAttacking = false;
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3  position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectCollider()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            //Debug.Log(collider.name);
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }
}
