using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    private HashSet<GameObject> emptyMirrors;
    public HashSet<GameObject> EmptyMirrors => emptyMirrors;

    private AudioSource levelBGM;
    public AudioSource LevelBGM => levelBGM;
    public AudioClip levelClip;
    public AudioClip chaseClip;

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
        levelBGM = gameObject.GetComponent<AudioSource>();
        LevelBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchBGM(int id)
    {
        switch (id)
        {
            case 0:
                LevelBGM.clip = levelClip;
                break;
            case 1:
                LevelBGM.clip = chaseClip;
                break;
            default:
                return;
        }
        LevelBGM.Play();
    }
}
