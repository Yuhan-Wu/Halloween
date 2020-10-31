using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float DisableTime = 5;
    [SerializeField]
    private GameObject slime = null;
    private Vector3 initSlimePos;

    private void Start()
    {
        initSlimePos = slime.transform.localPosition;
    }

    public void ShowSlime()
    {
        slime.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Release()
    {
        slime.transform.localPosition = initSlimePos;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(RestoreCollision());
    }

    IEnumerator RestoreCollision()
    {
        yield return new WaitForSeconds(DisableTime);

        GetComponent<Collider>().enabled = true;
    }
}
