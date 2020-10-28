using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermittentTile : MonoBehaviour
{
    public float Appear = 0;
    public float Disappear = 0;
    public int Damage = 0;

    private bool Activated = true;

    private bool isCoroutineExecuting = false;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;
        float waitTime = Activated ? Appear : Disappear;
        yield return new WaitForSeconds(waitTime);

        Activated = !Activated;
        GetComponent<Collider>().enabled = Activated;
        GetComponent<MeshRenderer>().enabled = Activated;
        isCoroutineExecuting = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag  == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().HP -= Damage;
        }
    }
}
