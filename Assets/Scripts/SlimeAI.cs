using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing,
        Returning
    }

    [Header("Speed")]
    [SerializeField] private float roamSpeed = 1f;
    [SerializeField] private float chaseSpeed = 1f;
    [SerializeField] private float returnSpeed = 1.5f;

    [Header("Roaming")]
    [SerializeField] private float roamingRadius = 3f;
    [SerializeField] private float roamingInterval = 2f;

    [Header("Chasing")]
    [SerializeField] private float chaseDistance = 4f;
    [SerializeField] private float stopChaseDistance = 6f;

    [Header("Returning")]
    [SerializeField] private float returnStopDistance = 0.2f;


    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 spawnPosition;
    private Transform player;
    private Coroutine roamingCoroutine;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPosition = transform.position;
        state = State.Roaming;
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Roaming:
                enemyPathfinding.SetSpeed(roamSpeed);
                if (distance < chaseDistance)
                {
                    StopRoaming();
                    state = State.Chasing;
                }
                break;
            case State.Chasing:
                enemyPathfinding.SetSpeed(chaseSpeed);
                if (distance > stopChaseDistance)
                {
                    state = State.Returning;
                }
                else
                {
                    ChasePlayer();
                }
                break;   
            case State.Returning:
                enemyPathfinding.SetSpeed(returnSpeed);
                ReturnToSpawn();
                if (Vector2.Distance(transform.position, spawnPosition) < returnStopDistance)
                {
                    state = State.Roaming;
                    StartRoaming();
                }
                break;
        }
    }

    //====ROAMING====
    private void StartRoaming()
    {
        roamingCoroutine = StartCoroutine(RoamingRoutine());
    }
    private void StopRoaming()
    {
        if (roamingCoroutine != null)
        {
            StopCoroutine(roamingCoroutine);
            roamingCoroutine = null;
        }
    }
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamTarget = GetRandomRoamingPosition();
            MoveTowards(roamTarget);
            yield return new WaitForSeconds(roamingInterval);
        }
    }

    private Vector2 GetRandomRoamingPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamingRadius;
        return spawnPosition + randomOffset;
    }
    private void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        enemyPathfinding.MoveTo(direction);
    }

    //====CHASING====
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        enemyPathfinding.MoveTo(direction);
    }

    //====RETURN TO SPAWN====
    private void ReturnToSpawn()
    {
        Vector2 dir = (spawnPosition - (Vector2)transform.position).normalized;
        enemyPathfinding.MoveTo(dir);
    }
    //====DEBUG====
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPosition, roamingRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
