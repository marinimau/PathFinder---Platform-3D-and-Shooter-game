using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrol : MonoBehaviour
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
    public Animator animEnemy;
    public bool isLamabile;
    public GameObject enemy;
    public ParticleSystem blood;
    public ParticleSystem bloodBody;

    public ParticleSystem fire;

    public bool isFiring;
    public float fireTimer;

    public float life;
    public bool isDead;
    public bool killOk;


    public float cadenzaFuoco = 1f;

    public AudioSource enemyFireSound;

    public GameObject zonaLama;

    public AudioSource hitSound;

    public bool bodyHit;
    public bool headHit;




    // Start is called before the first frame update
    void Start()
    {
        /*------------------------
         *  inizializzo la navmesh
         * -----------------------*/
        navMesh = GetComponent<NavMeshAgent>();
        animEnemy = transform.GetComponent<Animator>();
        navMesh.speed = speed;
        navMesh.autoBraking = false;
        waitTime = startWaitTime;
        navMesh.updateRotation = false;
        isLamabile = false;
        isFiring = false;
        isDead = false;
        killOk = false;
        //fuoco.enableEmission = false;
        life = 100f;

        zonaLama = gameObject.transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player");
        animEnemy = navMesh.gameObject.GetComponentInChildren<Animator>();
        animEnemy.SetBool("isWalking", true);
        animEnemy.SetFloat("speedPercentage", 1);
        /*------------------------
         *  seleziono a caso il primo punto del giro di pattuglia
         * -----------------------*/
        randomSpots = Random.Range(0, moveSpots.Length);


        bodyHit = false;
        headHit = false;

    }

    private void Update()
    {
        if (life == 0)
            isDead = true;
        if (navMesh.velocity != Vector3.zero)
            animEnemy.SetFloat("speedPercentage", 1);
        else
            animEnemy.SetFloat("speedPercentage", 0);
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
                *  se il nemico non ci vede
                * -----------------------*/
            if (CharacterControllerScript.player_contact_deactivated)
            {
                //se il player è sfuggito
                navMesh.SetDestination(moveSpots[randomSpots].position);
                waitTime = 0;
                animEnemy.SetBool("isShooting", false);
            }


            if ((!navMesh.pathPending && navMesh.remainingDistance < 0.5f) || isDead)
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
                        randomSpots = Random.Range(0, moveSpots.Length);
                    }
                    navMesh.SetDestination(moveSpots[randomSpots].position);
                    navMesh.speed = 1;
                    animEnemy.SetBool("isWalking", true);
                    setWait = false;

                }
                else
                {
                    /*------------------------
                        *  aspetta nel waypoint
                        * -----------------------*/
                    waitTime -= Time.deltaTime * 0.01f;
                    navMesh.speed = 0;
                    animEnemy.SetBool("isWalking", false);
                    animEnemy.SetFloat("speedPercentage", 0);
                    //qua deve guardarsi attorno
                }

            }
            else
            {
               /*------------------------
                *  cambio di posizione
                * -----------------------*/

                navMesh.destination = moveSpots[randomSpots].position;
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
            navMesh.destination = player.transform.position;
            transform.LookAt(player.transform.position + (new Vector3(0, 1f, 0)));
            navMesh.stoppingDistance = 10;
            if (navMesh.remainingDistance < 50)
            {
                //se è abbastanza vicino spara
                fireOnPlayer();

            }
        }

        if (headHit)
        {
            animEnemy.SetBool("isHeadHit", true);
            blood.Play();
            if (life > 0)
            {
                decrLife(100);
            }
            headHit = false;

        }

        if (bodyHit)
        {
            bloodBody.Play();
            if (life > 0)
            {
                if(CharacterControllerScript.specialBullet){
                    decrLife(50*3);
                } else{
                    decrLife(50);
                }
                if(life>0){
                    CharacterControllerScript.player_contact = true;
                    hitSound.Play();
                }

            }
            bodyHit = false;

        }
    }

    void fireOnPlayer()
    {
        RaycastHit hit;
        Vector3 fucile = navMesh.transform.position;
        fucile.y += 0.5f;
        if (!isFiring && CharacterControllerScript.player_contact)
        {
            isFiring = true;
            fireTimer = Random.Range(0, 5);
            if (Physics.Raycast(fucile, navMesh.transform.forward, out hit))
            {
                animEnemy.SetBool("isShooting", true);
                fire.enableEmission = true;
                fire.Play();
                enemyFireSound.Play();
                Debug.Log("Enemy Fire");
                Debug.DrawRay(fucile, navMesh.transform.forward * 10, Color.green);
                Debug.Log("Nemico colpisce: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    if(!CharacterControllerScript.immortality){
                        CharacterControllerScript.decrHealth(16);
                        if(CharacterControllerScript.isDead){
                            ShowMessage.id = 8;
                        }
                    }
                    CharacterControllerScript.PlayerBlood.Play();
                    Debug.Log("Player hit");
                    Talk.id = 2;
                }
            }
        }
        fireTimer -= Time.deltaTime * cadenzaFuoco;
        if (fireTimer <= 0)
        {
            isFiring = false;
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
        navMesh.isStopped = true;
        if (animEnemy.GetBool("isHeadHit") == false)
            animEnemy.SetBool("isDead", true);
        if (!isDead)
        {
            isDead = true;
            killOk = true;
        }
    }


    public void stopEnemy()
    {
        this.speed = 0f;
        navMesh.isStopped = true;
        animEnemy.SetFloat("speedPercentage", 0.1f);
    }

}
