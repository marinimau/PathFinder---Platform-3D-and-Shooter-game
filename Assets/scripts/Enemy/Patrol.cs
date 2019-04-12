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
    public float startWaitTime=5;

    public bool contact_status=false;
    public float gravity = -12;
    public NavMeshAgent navMesh;
    public GameObject player;
    public bool isLamabile;


    // Start is called before the first frame update
    void Start()
    {
        /*------------------------
         *  inizializzo la navmesh
         * -----------------------*/
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
        navMesh.autoBraking = false;
        waitTime = startWaitTime;
        navMesh.updateRotation = false;
        isLamabile = false;


        /*------------------------
         *  seleziono a caso il primo punto del giro di pattuglia
         * -----------------------*/
        randomSpots = Random.Range(0, moveSpots.Length);

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!contact_status)
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
                    randomSpots = (randomSpots + 1) % moveSpots.Length;
                    waitTime = startWaitTime;
                    navMesh.SetDestination(moveSpots[randomSpots].position);

                }
                else
                {
                    /*------------------------
                     *  aspetta nel waypoint
                     * -----------------------*/
                    waitTime -= Time.deltaTime;
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
        }


    }
}
