using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    private bool isRunning = false;
    public float maxStamina = 5f;
    public float stamina;
    public StaminaBar staminaBar;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        staminaBar.setMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        staminaBar.SetStamina(stamina);

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift)&&(stamina>0))
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
            speed = 12f;
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime;
            }
            else if (stamina >= maxStamina)
            {
                stamina = 5f;
            }
        }
        
        Debug.Log(stamina+" "+speed);
    }
}
