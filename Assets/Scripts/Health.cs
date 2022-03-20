using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 4;
    public Animator anim;
    public Canvas deathCanvas;
    public Canvas phoneCanvas;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
        if (transform.tag == "Player")
        {
            phoneCanvas = GameObject.Find("PhoneCanvas").GetComponent<Canvas>();
            deathCanvas = GameObject.Find("Death Canvas").GetComponent<Canvas>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if(transform.tag== ("Player"))
            {
                deathCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            anim.SetBool("Death", true);
        }
        if (Input.GetKeyDown(KeyCode.X) && phoneCanvas.enabled == true)
        {
            phoneCanvas.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.X)) 
        {
            phoneCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }   
        
        
    }
    public void takeDamage(int i)
    {
        health -= i;
        anim.SetBool("Hit",true);
    }
}
