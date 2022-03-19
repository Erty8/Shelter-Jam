using System;
using System.Diagnostics;
using UnityEngine;

public class GunScript : MonoBehaviour
{


    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public int ammo = 13;
    public int maxAmmo;
    public int reserveAmmo = 26;
    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public Animator anim;

    private float nextTimeToFire = 0f;
    private void Start()
    {
        maxAmmo = ammo;
        anim = GameObject.Find("PlayerBody").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammo < maxAmmo)
        {
            anim.SetBool("Reload", true);
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammo>0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            anim.SetBool("Fire", true);
        }
       
        else if  (Input.GetButton("Fire1") && ammo == 0) 
            {
            anim.SetBool("Reload", true);
        }
        else
        {
            anim.SetBool("Fire", false);
        }




    }

    void Shoot()
    {
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

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            if (ammo >= 0)
            {
                ammo--;
            }
            
            //Destroy(impactGO, 2f);
        }

    }
}


