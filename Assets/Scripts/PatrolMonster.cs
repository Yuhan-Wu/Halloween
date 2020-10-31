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
    private Transform[] waypoints = null;
    int currentWaypoint = 0;

    public float PatrolSpeed = 5;
    public float ChasingSpeed = 10;

    private void Start()
    {
        currentState = EnemyStates.Patrolling;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = PatrolSpeed;
    }

    void Update()
    {
        if (currentState != EnemyStates.Stuck)
        {
            if (Vector3.Distance(transform.position, objectToChase.position) > stopChaseDis)
            {
                if (currentState == EnemyStates.Chasing)
                {
                    currentState = EnemyStates.Patrolling;
                    agent.speed = PatrolSpeed;
                    LevelManager.Instance.SwitchBGM(0);
                }
            }
            else
            {
                if (currentState == EnemyStates.Patrolling && Vector3.Distance(transform.position, objectToChase.position) < startChaseDis)
                {
                    currentState = EnemyStates.Chasing;
                    agent.speed = ChasingSpeed;
                    LevelManager.Instance.SwitchBGM(1);
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
        else
        {
            agent.velocity = Vector3.zero;
            stuckTime -= Time.deltaTime;
            if (stuckTime <= 0)
            {
                currentState = prevState;
            }
        }
    }

}
