using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{


    public float damage = 1f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public static int ammo = 13;
    public static int maxAmmo = 13 ;
    public static int reserveAmmo = 26;
    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public Animator anim;
    bool canreload = true;
    public bool canshoot = true;

    private float nextTimeToFire = 0f;
    private void Start()
    {
        maxAmmo = ammo;
        anim = GameObject.Find("PlayerBody").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
        

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo>0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            anim.SetBool("Fire", true);
        }
        else if (Input.GetKeyDown(KeyCode.R) && (maxAmmo>ammo))
        {
            if (canreload)
            {
                anim.SetBool("Reload", true);
            }
            
        }

        else if  (Input.GetButtonDown("Fire1")) 
            {
            if (canreload && ammo == 0)
            {
                anim.SetBool("Reload", true);
            }
            
        }
        else
        {
            anim.SetBool("Fire", false);
        }

    }
    
    IEnumerator cannotReload()
    {
        canreload = false;
        yield return new WaitForSeconds(4);
        canreload = true;
    }
    void Shoot()
    {
        canshoot = false;
        //muzzleflash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);

            //Target target = hit.transform.GetComponent<Target>();
            //if (target != null)
            //{
            //    target.TakeDamage(damage);
            //}

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Health>().takeDamage(1);
            }
            
            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            if (ammo >= 0)
            {
                ammo--;
            }
            
            //Destroy(impactGO, 2f);
        }

    }
}


