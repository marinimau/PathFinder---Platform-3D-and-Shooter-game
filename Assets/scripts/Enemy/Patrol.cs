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
    public float startWaitTime=20;

    public float gravity = -12;
    public NavMeshAgent navMesh;
    public GameObject player;
    private Animator anim;
    private Animator animEnemy;
    public bool isLamabile;
    public GameObject enemy;

    public bool isFiring;
    public float fireTimer;

    public float cadenzaFuoco=1f;


    // Start is called before the first frame update
    void Start()
    {
        /*------------------------
         *  inizializzo la navmesh
         * -----------------------*/
        navMesh = GetComponent<NavMeshAgent>();
        anim = player.GetComponent<Animator>();
        animEnemy = transform.GetComponent<Animator>();
        navMesh.speed = speed;
        navMesh.autoBraking = false;
        waitTime = startWaitTime;
        navMesh.updateRotation = false;
        isLamabile = false;
        isFiring = false;
        player = GameObject.FindGameObjectWithTag("Player");
        animEnemy = navMesh.gameObject.GetComponentInChildren<Animator>();
        anim.SetBool("isWalking", true);
        /*------------------------
         *  seleziono a caso il primo punto del giro di pattuglia
         * -----------------------*/
        randomSpots = Random.Range(0, moveSpots.Length);


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!EnemySight.player_contact)
        {
                /*------------------------
                 *  se il nemico non ci vede
                 * -----------------------*/


            if (!navMesh.pathPending && navMesh.remainingDistance < 0.5f)
            {
                /*------------------------
                 *  se è in posizione
                 * -----------------------*/
                if (waitTime <= 0)
                {
                    /*------------------------
                     *  vai alla prossima posizione
                     * -----------------------*/
                    //randomSpots =(Random.Range(0, moveSpots.Length);
                    if (moveSpots.Length == 0) {
                        randomSpots = 0;
                    } else {
                        randomSpots = Random.Range(0, moveSpots.Length);
                    }
                    waitTime = startWaitTime;
                    navMesh.SetDestination(moveSpots[randomSpots].position);

                }
                else
                {
                    /*------------------------
                     *  aspetta nel waypoint
                     * -----------------------*/
                    waitTime -= Time.deltaTime;
                    anim.SetBool("isWalking", false);
                    //qua deve guardarsi attorno
                }

            } else{
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

        } else {

            /*------------------------
             *  se siamo stati visti dal nemico
             * -----------------------*/
            navMesh.destination = player.transform.position;
            transform.LookAt(player.transform.position);
            navMesh.stoppingDistance = 10;
            if(navMesh.remainingDistance<50){
                //se è abbastanza vicino spara
                fireOnPlayer();

            }
        }

        if (animEnemy.GetBool("isHeadHit") == true)
            navMesh.enabled = false;
            //navMesh.speed = 0;
        
    }

    void fireOnPlayer()
    {
        RaycastHit hit;
        Vector3 fucile = navMesh.transform.position;
        fucile.y += 0.5f;
        if(!isFiring){
            isFiring = true;
            fireTimer = 1f;
            if (Physics.Raycast(fucile, navMesh.transform.forward, out hit))
            {
                Debug.Log("Enemy Fire");
                Debug.DrawRay(fucile, navMesh.transform.forward * 10, Color.green);
                Debug.Log("Nemico colpisce: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Player")
                {
                    CharacterControllerScript.decrHealth(25);
                    Debug.Log("Player hit");
                }
            }
        }
        fireTimer -= Time.deltaTime*cadenzaFuoco;
        if(fireTimer<=0){
            isFiring = false;
        }
    }

}
