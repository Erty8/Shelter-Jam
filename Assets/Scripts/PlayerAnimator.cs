using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GunScript gunscript;
    int ammoUsed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gunscript = GameObject.Find("GunRay").GetComponent<GunScript>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void reload()
    {
        Debug.Log("reloaded");
        ammoUsed = gunscript.maxAmmo - gunscript.ammo;
        gunscript.ammo = gunscript.maxAmmo;
        gunscript.reserveAmmo -= ammoUsed;
        anim.SetBool("Reload", false);
    }
    void canShoot()
    {
        Debug.Log("can shoot");
        gunscript.canshoot = true;
    }
}
