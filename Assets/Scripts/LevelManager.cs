using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    private HashSet<GameObject> emptyMirrors;
    public HashSet<GameObject> EmptyMirrors => emptyMirrors;

    private AudioSource levelBGM;
    private AudioSource loopSoundFX;
    private AudioSource soundFX;
    public AudioClip levelClip;
    public AudioClip chaseClip;
    public AudioClip heartBitClip;
    public AudioClip getCandySound;

    private int chasingMonsterNum = 0;
    public GameObject RushText;
    private bool initRush = false;

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
        levelBGM = gameObject.GetComponents<AudioSource>()[0];
        levelBGM.clip = levelClip;
        levelBGM.Play();
        soundFX = gameObject.GetComponents<AudioSource>()[1];
        loopSoundFX = gameObject.GetComponents<AudioSource>()[2];
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
                levelBGM.clip = levelClip;
                break;
            case 1:
                levelBGM.clip = chaseClip;
                break;
            default:
                return;
        }
        levelBGM.Play();
    }

    public void PlaySoundFX(int id)
    {
        switch (id)
        {
            case 0:
                soundFX.clip = getCandySound;
                break;
            default:
                return;
        }
        soundFX.Play();
    }

    public void PlayLoopSoundFX(int id)
    {
        switch (id)
        {
            case 0:
                loopSoundFX.clip = heartBitClip;
                break;
            default:
                return;
        }
        loopSoundFX.Play();
    }

    public void StopLoopSoundFX()
    {
        loopSoundFX.Stop();
    }

    public void UpdateChase(bool add)
    {
        if (add)
        {
            if (chasingMonsterNum == 0)
            {
                SwitchBGM(1);
                PlayLoopSoundFX(0);
                if (!initRush)
                {
                    initRush = true;
                    RushText.SetActive(true);
                    StartCoroutine(HideRushText());
                }
            }
            chasingMonsterNum++;
        }
        else
        {
            chasingMonsterNum--;
            if (chasingMonsterNum == 0)
            {
                SwitchBGM(0);
                StopLoopSoundFX();
            }
        }
    }

    private IEnumerator HideRushText()
    {
        yield return new WaitForSeconds(2f);
        RushText.SetActive(false);
    }
}
