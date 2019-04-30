﻿using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boris : MonoBehaviour
{
    public float speed = 1;
    public Transform[] moveSpots;
    private int randomSpots;

    private float waitTime;
    private bool setWait;
    public float startWaitTime = 1;

    public float gravity = -12;
    public NavMeshAgent navMesh;
    public GameObject player;
    private Animator animBoris;
    public bool isLamabile;
    public GameObject boris;
    public ParticleSystem blood;
    public ParticleSystem bloodBody;


    public bool isPunching;
    public float punchTimer;

    public float life;
    public bool isDead;
    public bool killOk;
   

    public AudioSource borisPunchSound;

    public GameObject zonaLama;

    public AudioSource hitSound;

    public bool bodyHit;
    public bool headHit;

    public float cadenzaPunch = 1f;



    // Start is called before the first frame update
    void Start()
    {
        /*------------------------
         *  inizializzo la navmesh
         * -----------------------*/
        navMesh = GetComponent<NavMeshAgent>();
        animBoris = transform.GetComponent<Animator>();
        navMesh.speed = speed;
        navMesh.autoBraking = false;
        waitTime = startWaitTime;
        navMesh.updateRotation = false;
        isLamabile = false;
        isPunching = false;
        isDead = false;
        killOk = false;
        //fuoco.enableEmission = false;
        life = 100f;

        zonaLama = gameObject.transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player");
        animBoris = navMesh.gameObject.GetComponentInChildren<Animator>();
        animBoris.SetBool("isWalking", true);
        animBoris.SetBool("playerContact", false);
        animBoris.SetFloat("speedPercentage", 1);
        /*------------------------
         *  seleziono a caso il primo punto del giro di pattuglia
         * -----------------------*/
        //randomSpots = Random.Range(0, moveSpots.Length);

        bodyHit = false;

    }

    private void Update()
    {
        if (life == 0)
            isDead = true;
        if (navMesh.velocity != Vector3.zero){
            animBoris.SetFloat("speedPercentage", 1);
        }
        else{
            animBoris.SetFloat("speedPercentage", 0);
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isDead && !killOk)
        {
            kill();
        }


        if (!CharacterControllerScript.player_contact || isDead)
        {
            /*------------------------
             *  se boris non ci vede
             * -----------------------*/
            if (CharacterControllerScript.player_contact_deactivated)
            {
                //se il player è sfuggito
                //navMesh.SetDestination(moveSpots[randomSpots].position);
                waitTime = 0;
                animBoris.SetBool("playerContact", false);
            }


            if (!navMesh.pathPending && navMesh.remainingDistance < 0.5f)
            {
                /*------------------------
                 *  se è in posizione
                 * -----------------------*/
                if (!setWait)
                {
                    setWait = true;
                    waitTime = startWaitTime;
                }

                if (waitTime <= 0)
                {
                    /*------------------------
                     *  vai alla prossima posizione
                     * -----------------------*/
                    //randomSpots =(Random.Range(0, moveSpots.Length);
                    if (moveSpots.Length == 0)
                    {
                        randomSpots = 0;
                    }
                    else
                    {
                        //randomSpots = Random.Range(0, moveSpots.Length);
                    }
                    //navMesh.SetDestination(moveSpots[randomSpots].position);
                    navMesh.speed = 1;
                    setWait = false;

                }
                else
                {
                    /*------------------------
                     *  aspetta nel waypoint
                     * -----------------------*/
                    waitTime -= Time.deltaTime * 0.01f;
                    animBoris.SetBool("isWalking", false);
                    animBoris.SetFloat("speedPercentage", 0);
                    //qua deve guardarsi attorno
                }

            }
            else
            {
                /*------------------------
                 *  cambio di posizione
                 * -----------------------*/

                //navMesh.destination = moveSpots[randomSpots].position;
                if (navMesh.velocity.sqrMagnitude > Mathf.Epsilon)
                {
                    transform.rotation = Quaternion.LookRotation(navMesh.velocity.normalized);
                }
                //navMesh.transform.LookAt(moveSpots[randomSpots].position);
            }

        }
        else
        {

            /*------------------------
             *  se siamo stati visti dal nemico
             * -----------------------*/
            animBoris.SetBool("playerContact", false);
            navMesh.destination = player.transform.position;
            transform.LookAt(player.transform.position + (new Vector3(0, 1f, 0)));
            navMesh.stoppingDistance = 0.5f;
            navMesh.speed = 4;
            if (navMesh.remainingDistance < 0.6)
            {
                //se è abbastanza vicino picchia
                punchOnPlayer();

            }
            if(navMesh.speed==0){
                animBoris.SetBool("isWalking", true);
            } else{
                animBoris.SetBool("isWalking", true);
            }
        }

        if (headHit)
        {
            blood.Play();
            kill();

        }

        if (bodyHit)
        {
            bloodBody.Play();
            if (life > 0)
            {
                decrLife(34);
                CharacterControllerScript.player_contact = true;
                hitSound.Play();
            }
            bodyHit = false;

        }

    }

    void punchOnPlayer()
    {
        RaycastHit hit;
        Vector3 fucile = navMesh.transform.position;
        fucile.y += 0.5f;
        if (!isPunching && CharacterControllerScript.player_contact)
        {
            isPunching = true;
            punchTimer = Random.Range(0, 5);
            if (Physics.Raycast(fucile, navMesh.transform.forward, out hit))
            {
                //animBoris.SetBool("isShooting", true);
                //borisPunchSound.Play();
                if(Random.Range(0,2)%2==0){
                    animBoris.SetBool("isPunching", true);
                } else {
                    animBoris.SetBool("isKicking", true);
                }
                Debug.Log("Boris punch");
                Debug.DrawRay(fucile, navMesh.transform.forward * 10, Color.green);
                Debug.Log("Boris colpisce: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    if (!CharacterControllerScript.immortality)
                    {
                        CharacterControllerScript.decrHealth(16);
                        if (CharacterControllerScript.isDead)
                        {
                            ShowMessage.id = 8;
                        }
                    }
                    CharacterControllerScript.PlayerBlood.Play();
                    Debug.Log("Player hit");
                    Talk.id = 2;
                }
            }
        }
        punchTimer -= Time.deltaTime * cadenzaPunch;
        if (punchTimer <= 0)
        {
            isPunching = false;
            animBoris.SetBool("isPunching", false);
            animBoris.SetBool("isKicking", false);
        }
    }

    public void decrLife(int damage)
    {
        if (life - damage > 0)
        {
            life -= damage;
        }
        else
        {
            life = 0;
            kill();
        }

    }

    public void incLife(int cura)
    {
        if (life + cura <= 100)
        {
            life += cura;
        }
        else
        {
            life = 100;
        }
    }

    public void kill()
    {
        ShowMessage.id = 0;
        speed = 0;

        animBoris.SetBool("isDead", true);
        Destroy(zonaLama);
        Destroy(navMesh);
        navMesh.enabled = false;
        if (!isDead)
        {
            isDead = true;
            killOk = true;
        }

        Destroy(navMesh);
        Destroy(transform);
    }


    public void stopEnemy()
    {
        this.speed = 0f;
        navMesh.isStopped = true;
        //animBoris.SetFloat("speedPercentage", 0.1f);
    }

}
