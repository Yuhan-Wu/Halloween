using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMonster : Monster
{
    private NavMeshAgent agent;
    [SerializeField]
    private float stopChaseDis = 10f;
    [SerializeField]
    private Transform[] waypoints;
    int currentWaypoint = 0;

    public float PatrolSpeed = 5;
    public float ChasingSpeed = 10;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, objectToChase.position) > stopChaseDis)
        {
            if (currentState == EnemyStates.Chasing)
            {
                currentState = EnemyStates.Patrolling;
                agent.speed = PatrolSpeed;
            }
        }
        else
        {
            if (currentState == EnemyStates.Patrolling && Vector3.Distance(transform.position, objectToChase.position) < startChaseDis)
            {
                currentState = EnemyStates.Chasing;
                agent.speed = ChasingSpeed;
            }
            if (currentState == EnemyStates.Chasing)
                agent.SetDestination(objectToChase.position);
        }

        if (currentState == EnemyStates.Patrolling)
        {
            Vector2 monster = new Vector2(transform.position.x, transform.position.z);
            Vector2 dest = new Vector2(waypoints[currentWaypoint].position.x, waypoints[currentWaypoint].position.z);
            if (Vector2.Distance(monster, dest) <= 0.2f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}
