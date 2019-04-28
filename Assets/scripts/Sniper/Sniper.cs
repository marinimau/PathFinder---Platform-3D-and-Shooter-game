using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sniper : MonoBehaviour
{

    private float waitTime;
    private bool setWait;
    public float startWaitTime = 1;

    public float gravity = -12;
    public NavMeshAgent navMesh;
    public GameObject player;
    private Animator animEnemy;
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

    public AudioSource sniperFireSound;

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
        navMesh.speed = 0;
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
        //animEnemy = navMesh.gameObject.GetComponentInChildren<Animator>();
        //animEnemy.SetBool("isWalking", true);
        //animEnemy.SetFloat("speedPercentage", 1);

        bodyHit = false;
        headHit = false;

    }

    private void Update()
    {
        if (life == 0)
            isDead = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isDead && !killOk)
        {
            kill();
        }


        if (!CharacterControllerScript.player_contact)
        {
          /*-------------------------
           *  se il cecchino non ci vede si guarda attorno
           * -----------------------*/

        }
        else
        {

            /*------------------------
             *  se siamo stati visti dal nemico
             * -----------------------*/
            transform.LookAt(player.transform.position + (new Vector3(0, 1f, 0)));
            //spara
            fireOnPlayer();

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
                decrLife(50);
                hitSound.Play();
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
                animEnemy.SetBool("firing", true);
                //fire.enableEmission = true;
               
                Debug.DrawRay(fucile, navMesh.transform.forward * 10, Color.green);
                Debug.Log("Nemico colpisce: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    //fire.Play();
                    sniperFireSound.Play();
                    Debug.Log("Enemy Fire");
                    if(!CharacterControllerScript.immortality){
                        CharacterControllerScript.decrHealth(100);
                    }
                    CharacterControllerScript.PlayerBlood.Play();
                    Debug.Log("Player hit by sniper");
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

        navMesh.enabled = false;

        if (!isDead)
        {
            isDead = true;
            killOk = true;
        }

        animEnemy.SetBool("die", true);
        headHit = false;
        
    }


    public void stopEnemy()
    {
        /*
        navMesh.isStopped = true;
        animEnemy.SetFloat("speedPercentage", 0.1f);
        */
    }

}
