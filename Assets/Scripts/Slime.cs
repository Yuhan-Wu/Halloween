using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float DisableTime = 5;
    [SerializeField]
    private GameObject slime = null;

    private void Update()
    {
    }

    public void ShowSlime()
    {
        slime.SetActive(true);
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
