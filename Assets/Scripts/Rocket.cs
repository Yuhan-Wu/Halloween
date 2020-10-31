using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rocket : MonoBehaviour
{
    public Sprite noRocket;
    public Sprite getRocket;
    private bool ifRocket = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ifRocket = GameObject.Find("Player").GetComponent<PlayerMovement>().ifRocket;
        if (ifRocket)
        {
            GetComponent<Image>().sprite = getRocket;

        }
        else
        {
            GetComponent<Image>().sprite = noRocket;
        }
    }
}
