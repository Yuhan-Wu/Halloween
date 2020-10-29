using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    private GameObject curMirror;
    private GameObject prevMirror;
    [SerializeField]
    private float moveSpeed = 2f;

    protected override void Start()
    {
        base.Start();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(curMirror.transform.position, objectToChase.position) > startChaseDis)
        {
            if (currentState == EnemyStates.Chasing)
                currentState = EnemyStates.Idle;
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
        else
        {
            if (currentState == EnemyStates.Idle)
            {
                transform.position = curMirror.transform.Find("SpawnPoint").position;
                currentState = EnemyStates.Chasing;
            }
            Vector3 direction = objectToChase.position - transform.position;
            Vector3 rotationAngle = Quaternion.LookRotation(direction).eulerAngles;
            transform.rotation  = Quaternion.Euler(new Vector3(0, rotationAngle.y, 0));
            transform.Translate(moveSpeed * direction.normalized * Time.deltaTime, Space.World);
        }
    }
}
