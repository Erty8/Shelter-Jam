using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 4;
    public Animator anim;
    public Canvas deathCanvas;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        deathCanvas = GameObject.Find("Death Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            anim.SetBool("Death", true);
        }
    }
    public void takeDamage(int i)
    {
        health -= i;
        anim.SetBool("Hit",true);
    }
}
