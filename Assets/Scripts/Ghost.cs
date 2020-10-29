using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    private static bool init = false;
    private static HashSet<GameObject> emptyMirrors;
    [SerializeField]
    private GameObject curMirror;
    private GameObject prevMirror;
    [SerializeField]
    private float moveSpeed = 2f;

    private void Awake()
    {
        if (!init)
        {
            emptyMirrors = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("Mirror"));
            init = true;
        }      
    }

    protected override void Start()
    {
        base.Start();
        float minDis = float.MaxValue;
        foreach (GameObject mirror in emptyMirrors)
        {
            float dis = Vector3.Distance(transform.position, mirror.transform.position);
            if (dis < minDis)
                curMirror = mirror;
        }
        transform.position = curMirror.transform.position;
        transform.rotation = curMirror.transform.rotation;
        emptyMirrors.Remove(curMirror);
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
            foreach (GameObject mirror in emptyMirrors)
            {
                float dis = Vector3.Distance(objectToChase.position, mirror.transform.position);
                if (dis < minDis)
                    curMirror = mirror;
            }
            if (curMirror != prevMirror)
            {
                emptyMirrors.Add(prevMirror);
                emptyMirrors.Remove(curMirror);
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

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        init = false;
    }
}
