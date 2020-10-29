using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public bool Disable = false;
    public float DisableTime = 5;

    private bool isCoroutineExecuting = false;

    private void Update()
    {
        if (Disable)
        {
            StartCoroutine(DisableCollision());
        }
    }

    IEnumerator DisableCollision()
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(DisableTime);

        GetComponent<Collider>().enabled = true;
        isCoroutineExecuting = false;
        Disable = false;
    }
}
