using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [Header("Stat")]
    public int maxHealth;
    public float moveSpeed;

    [Header("Detection")]
    public float chaseRange;
    public float attackRange;
    public LayerMask targetLayer;

    [Header("Combat")]
    public int attackDamage;
    public float attackCooldown;
    public float knockbackTime;

}
