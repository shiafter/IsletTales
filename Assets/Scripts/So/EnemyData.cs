using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [Header("Stat")]
    public int maxHealth;
    public float moveSpeed;
    public EnemyType enemyType;

    [Header("Detection")]
    public float chaseRange;
    public float attackRange;
    public LayerMask targetLayer;

    [Header("Combat")]
    public int collisionDamage;
    public int meeleDamage;
    public int explodeDamage;
    public int rangeDamage;

    public float attackCooldown;
    public float knockbackTime;

    public enum EnemyType
    {
        Collision,
        Melee,
        Explode,
        Range
    }
}
