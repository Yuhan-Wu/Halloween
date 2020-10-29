using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CandyMonster : Monster
{
    private NavMeshAgent agent;
    [SerializeField]
    private Transform objectToChase = null;
    [SerializeField]
    private GameObject candy = null;
    [SerializeField]
    private GameObject ghost = null;
    //[SerializeField]
    //private Transform[] waypoints;
    //int currentWaypoint = 0;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, objectToChase.position) > startChaseDis)
        {
            if (currentState == EnemyStates.Chasing)
            {
                currentState = EnemyStates.Idle;
                candy.SetActive(true);
                ghost.SetActive(false);
                agent.velocity = Vector3.zero;
                agent.enabled = false;
            }
        }
        else
        {
            if (currentState == EnemyStates.Idle)
            {
                ghost.SetActive(true);
                candy.SetActive(false);
                currentState = EnemyStates.Chasing;
                agent.enabled = true;
            }
            agent.SetDestination(objectToChase.position);
        }

        //if (currentState == EnemyStates.Patrolling)
        //{
        //    if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.6f)
        //    {
        //        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        //    }
        //    agent.SetDestination(waypoints[currentWaypoint].position);
        //}
    }

}
