using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    private HashSet<GameObject> emptyMirrors;
    public HashSet<GameObject> EmptyMirrors => emptyMirrors;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        emptyMirrors = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("Mirror"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
