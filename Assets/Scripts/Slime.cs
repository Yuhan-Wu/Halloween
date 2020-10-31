using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public bool Disable = false;
    public float DisableTime = 5;
    [SerializeField]
    private GameObject slime = null;

    private bool isCoroutineExecuting = false;

    private void Update()
    {
    }

    public void ShowSlime()
    {
        slime.SetActive(true);
        Debug.Log("slime appear");
    }

    public void Release()
    {
        slime.SetActive(false);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(RestoreCollision());
    }

    IEnumerator RestoreCollision()
    {
        yield return new WaitForSeconds(DisableTime);

        GetComponent<Collider>().enabled = true;
    }
}
