using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    protected enum EnemyStates
    {
        Idle,
        Chasing,
        Patrolling,
        Stuck
    }

    protected EnemyStates currentState;
    [SerializeField]
    protected float startChaseDis = 10f;
    [SerializeField]
    protected Transform objectToChase = null;
    [SerializeField]
    protected float stuckTime = 2f;
    protected EnemyStates prevState;


    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentState == EnemyStates.Chasing)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShootRocket"))
        {
            prevState = currentState;
            currentState = EnemyStates.Stuck;
            Destroy(other.gameObject);
        }
    }
}
