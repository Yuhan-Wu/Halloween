using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    private GameObject curMirror;
    private GameObject prevMirror;
    [SerializeField]
    private float moveSpeed = 2f;
    private bool activated = false;
    [SerializeField]
    private GameObject ghost = null;

    private void Start()
    {
        float minDis = float.MaxValue;
        foreach (GameObject mirror in LevelManager.Instance.EmptyMirrors)
        {
            float dis = Vector3.Distance(transform.position, mirror.transform.position);
            if (dis < minDis)
                curMirror = mirror;
        }
        transform.position = curMirror.transform.position;
        transform.rotation = curMirror.transform.rotation;
        LevelManager.Instance.EmptyMirrors.Remove(curMirror);
        currentState = EnemyStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != EnemyStates.Stuck)
        {
            if (Vector3.Distance(curMirror.transform.position, objectToChase.position) > startChaseDis)
            {
                if (activated)
                {
                    if (currentState == EnemyStates.Chasing)
                    {
                        ghost.SetActive(false);
                        currentState = EnemyStates.Idle;
                        LevelManager.Instance.SwitchBGM(0);
                    }
                    prevMirror = curMirror;
                    float minDis = Vector3.Distance(objectToChase.position, curMirror.transform.position);
                    foreach (GameObject mirror in LevelManager.Instance.EmptyMirrors)
                    {
                        float dis = Vector3.Distance(objectToChase.position, mirror.transform.position);
                        if (dis < minDis)
                            curMirror = mirror;
                    }
                    if (curMirror != prevMirror)
                    {
                        LevelManager.Instance.EmptyMirrors.Add(prevMirror);
                        LevelManager.Instance.EmptyMirrors.Remove(curMirror);
                    }
                    transform.position = curMirror.transform.position;
                    transform.rotation = curMirror.transform.rotation;
                }
            }
            else
            {
                if (currentState == EnemyStates.Idle)
                {
                    if (!activated)
                        activated = true;
                    ghost.SetActive(true);
                    transform.position = curMirror.transform.Find("SpawnPoint").position;
                    currentState = EnemyStates.Chasing;
                    LevelManager.Instance.SwitchBGM(1);
                }
                if (currentState == EnemyStates.Chasing)
                {
                    Vector3 direction = objectToChase.position - transform.position;
                    Vector3 rotationAngle = Quaternion.LookRotation(direction).eulerAngles;
                    transform.rotation = Quaternion.Euler(new Vector3(0, rotationAngle.y, 0));
                    transform.Translate(moveSpeed * direction.normalized * Time.deltaTime, Space.World);
                }
            }
        }
        else
        {
            stuckTime -= Time.deltaTime;
            if (stuckTime <= 0)
            {
                currentState = prevState;
            }
        }
    }
}
