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
        Patrolling
    }

    protected EnemyStates currentState;
    [SerializeField]
    protected EnemyStates initState;
    [SerializeField]
    protected float startChaseDis = 10f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentState = initState;
    }

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
}
