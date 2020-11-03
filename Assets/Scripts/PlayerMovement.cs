using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    private bool isRunning = false;
    public float maxStamina = 5f;
    public float stamina;
    public StaminaBar staminaBar;
    public Text StrugglingPrompt;
    public bool Stuck = false;
    public float OriginalSpeed = 12f;
    public float StrugglingSpeed = 0.3f;
    private float LastStrugglingTime = 0;
    private int StrugglingCounter = 0;
    public int MaxStruggingCounter = 10;
    private Slime LastSlime = null;

    public int candyCount = 0;
    public bool ifRocket =false;

    public int HP = 100;
    public int MaxHP = 100;

    private bool reviveSpeed = false;

    public GameObject rocketPrefab;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        staminaBar.setMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (Stuck)
        {
            StrugglingPrompt.enabled = true;
            LastStrugglingTime += Time.deltaTime;
            speed = 0;
            if (LastStrugglingTime > StrugglingSpeed)
            {
                StrugglingCounter = 0;
                LastStrugglingTime = 0;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                LastStrugglingTime = 0;
                StrugglingCounter++;
                if (StrugglingCounter > MaxStruggingCounter)
                {
                    StrugglingCounter = 0;
                    Stuck = false;
                    // Disable slime for a while
                    if (LastSlime) LastSlime.Release();
                    reviveSpeed = true;
                }
            }
        }
        else
        {
            StrugglingPrompt.enabled = false;
            if (reviveSpeed)
            {
                speed = OriginalSpeed;
                reviveSpeed = false;
            }
            //speed = OriginalSpeed;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            staminaBar.SetStamina(stamina);

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.LeftShift) && (stamina > 0))
            {
                isRunning = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;
            }
            if (isRunning)
            {
                speed = 20f;
                stamina -= Time.deltaTime;
                if (stamina <= 0)
                {
                    stamina = 0;
                    isRunning = false;
                }
            }
            if (!isRunning)
            {
                speed = OriginalSpeed;
                if (stamina < maxStamina)
                {
                    stamina += Time.deltaTime;
                }
                else if (stamina >= maxStamina)
                {
                    stamina = 5f;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && ifRocket)
            {
                GameObject rocketObject = Instantiate(rocketPrefab);
                rocketObject.transform.position = transform.position;
                rocketObject.transform.forward = transform.forward;
                ifRocket = false;
            }

            //Debug.Log(stamina+" "+speed);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Candy")
        {
            stamina += 1f;
            candyCount++;
            Destroy(col.gameObject);
            LevelManager.Instance.PlaySoundFX(0);
            //Debug.Log(candySound);
        }else if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Spike")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(col.gameObject.tag == "Slime")
        {
            LastSlime = col.GetComponent<Slime>();
            LastSlime.ShowSlime();
            Stuck = true;
        }
        else if(col.gameObject.tag == "Rocket"&&ifRocket==false)
        {
            ifRocket = true;
            Destroy(col.gameObject);
        }
       // candySound = false;
    }


}
