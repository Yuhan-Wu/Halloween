using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CandyMonster : Monster
{
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject candy = null;
    [SerializeField]
    private GameObject monster = null;
    [SerializeField]
    private float stopChaseDis = 20f;
    //[SerializeField]
    //private Transform[] waypoints;
    //int currentWaypoint = 0;
    private EnemyStates prevState;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        if (Vector3.Distance(transform.position, objectToChase.position) > stopChaseDis)
        {
            if (currentState == EnemyStates.Chasing)
            {
                currentState = EnemyStates.Idle;
                candy.SetActive(true);
                monster.SetActive(false);
                agent.velocity = Vector3.zero;
                agent.enabled = false;
            }
        }
        if(currentState == EnemyStates.stuck)
        {
            
            agent.velocity = Vector3.zero;
            stuckTime -= Time.deltaTime;
            if (stuckTime <= 0)
            {
                currentState = prevState;
            }
        }
        else
        {
            if (currentState == EnemyStates.Idle && Vector3.Distance(transform.position, objectToChase.position) < startChaseDis)
            {
                monster.SetActive(true);
                candy.SetActive(false);
                currentState = EnemyStates.Chasing;
                agent.enabled = true;
            }
            if (currentState == EnemyStates.Chasing)
                agent.SetDestination(objectToChase.position);
        }
        //Debug.Log(currentState);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ShootRocket")
        {
            prevState = currentState;
            currentState = EnemyStates.stuck;
            Destroy(col.gameObject);
        }
    }

}
