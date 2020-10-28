using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCount : MonoBehaviour
{
    public Text candyCountText;
    private int candyCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        candyCount = GameObject.Find("Player").GetComponent<PlayerMovement>().candyCount;
        candyCountText.text = "Candy: " + candyCount;
    }
}
