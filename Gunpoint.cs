﻿
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Gunpoint : MonoBehaviour
{
    public float damage = 10f;
    public float range = 1000f;
    public ParticleSystem muzzleflashee;

    public AudioSource reloadsound;
    public AudioSource emptyammosound;
    public AudioSource firingsound;


    public GameObject metaleffect;
    public GameObject groundeffect;
    public GameObject enemyeffect;
    public GameObject cementeffect;
    public GameObject woodeffect;
    public GameObject grasseffect;

    public float nextFire = 0.2f;
    public float firerate = 10f;

    public Camera fpscam;

   

    public Text carryammmotext;
    public Text currentammmotext;
    public Text magazinammmotext;
    public Text Reloadtext;

    public Image gun1img;
    public Image gun11img;


    public static int totalammo =75;
    
    public int maxAmmo = 25;
    private int defammo = 0;
    public static int currentAmmo =25;
    public float reloadtime = 1f;
    private bool isreloading = false;

    

    void Start()

    {
        //Camera fpscam = GameObject.FindWithTag("fpscam").GetComponent<Camera>();
        Reloadtext.enabled = false;
        
        //if (currentAmmo==-1)
        //currentAmmo = maxAmmo;

    }

   void OnEnable()
    {
        isreloading = false;
    }

    void Update()
    {
        if (totalammo <= 0)
        {
            carryammmotext.text = defammo.ToString();
        }


        if (currentAmmo <= 0 && totalammo <= 0)
        {
                magazinammmotext.text = defammo.ToString();
                currentammmotext.text = defammo.ToString();
                gun1img.color = Color.red;
                gun11img.color = Color.red;
                if (Input.GetButtonDown("Fire1"))
                {
                  if(!emptyammosound.isPlaying)
                  {
                    emptyammosound.Play();
                  }
                    
                }
                return;
        }
           
        

        

        if (totalammo >= 0)
        {
            magazinammmotext.text =maxAmmo.ToString();
            carryammmotext.text = totalammo.ToString();
            gun1img.color = Color.white;
            gun11img.color = Color.white;
        }

        if (currentAmmo != 0)
        {
            currentammmotext.text = currentAmmo.ToString();
        }



        if (isreloading)
        {
           
            scopeview.iszoom= false;
            
            return;
        }

        

        if (currentAmmo<=0)
        {
            StartCoroutine(reload());

            totalammo = totalammo-maxAmmo;
            currentAmmo = maxAmmo;
            currentammmotext.text = currentAmmo.ToString();
            carryammmotext.text = totalammo.ToString();
            return;
        }

        if(totalammo >= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(reload());

                totalammo = totalammo - (maxAmmo-currentAmmo);
                currentAmmo = maxAmmo;
                currentammmotext.text = currentAmmo.ToString();
                carryammmotext.text = totalammo.ToString();
                return;

               
            }
        }

        if (carcontroller.isrun == false)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / firerate;
                
                Fire();
            }
        }

    }

    IEnumerator reload()
    {
        isreloading = true;
        reloadsound.Play();
        Reloadtext.enabled =true;

        yield return new WaitForSeconds(reloadtime);

        Reloadtext.enabled =false;
        reloadsound.Stop();
        isreloading = false;
    }

    void Fire()
    {
       muzzleflashee.Play();
        firingsound.Play();

        currentAmmo--;
        currentammmotext.text = currentAmmo.ToString();

        Vector3 rayorigin =fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        

        RaycastHit hit;

        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "enemy")
        {


            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.takedamage(damage);
               

            }


            GameObject enemyeffect11 = Instantiate(enemyeffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(enemyeffect11, 1f);

        }





        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "metalmetal")
        {
            GameObject metaleffect11 = Instantiate(metaleffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(metaleffect11, 1f);
        }




        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "Ground")
        { 
            GameObject groundeffect11 = Instantiate(groundeffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(groundeffect11, 1f);

        }


        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "cementcement")
        {
            GameObject cementeffect11 = Instantiate(cementeffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(cementeffect11, 1f);
        }


        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "woodwood")
        {
            GameObject woodeffect11 = Instantiate(woodeffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(woodeffect11, 1f);
        }

        if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, range) && hit.transform.tag == "grass")
        {
            GameObject grasseffect11 = Instantiate(grasseffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(grasseffect11, 1f);
        }



    }

   
}

