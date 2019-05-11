using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss : MonoBehaviour
{
    public float speed = 0;
    
    public float gravity = -12;
    public NavMeshAgent navMesh;
    public GameObject player;
    private Animator animBoss;
    public GameObject boss;
    public ParticleSystem blood;
    public ParticleSystem bloodBody;

    public ParticleSystem fire;

    public bool isFiring;
    public float fireTimer;

    public static float life;
    public bool isDead;
    public bool killOk;


    public float cadenzaFuoco = 1f;

    public AudioSource bossFireSound;
    public AudioSource reloadSound;
    public AudioSource hitSound;

    public bool bodyHit;
    public bool headHit;

    public bool setPlayerContact;

    public int nColpi;
    private int nColpiStart;




    // Start is called before the first frame update
    void Start()
    {
        /*------------------------
         *  inizializzo la navmesh
         * -----------------------*/
        navMesh = GetComponent<NavMeshAgent>();
        //animBoss = transform.GetComponent<Animator>();
        navMesh.speed = speed;
        navMesh.autoBraking = false;
        navMesh.updateRotation = false;
        isFiring = false;
        isDead = false;
        killOk = false;
        //fuoco.enableEmission = false;
        life = 100f;
        setPlayerContact = false;
        nColpiStart = 8;
        nColpi = 8;


        player = GameObject.FindGameObjectWithTag("Player");
        animBoss = navMesh.gameObject.GetComponentInChildren<Animator>();
        animBoss.SetFloat("speedPercentage", 0);
        /*------------------------
         *  seleziono a caso il primo punto del giro di pattuglia
         * -----------------------*/

        bodyHit = false;
        headHit = false;

    }

    private void Update()
    {
        if (life == 0)
            isDead = true;
        if (navMesh.velocity != Vector3.zero){
            animBoss.SetFloat("speedPercentage", 2);
        }
       
        else {
            animBoss.SetFloat("speedPercentage", 0);
        }

        if(CharacterControllerScript.boss_contact){
            BossLife.show = true;
        }
        else{
            BossLife.show = false;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isDead && !killOk)
        {
            kill();
        }


        if (!CharacterControllerScript.boss_contact)
        {
            /*------------------------
             *  se il boss non ci vede
             * -----------------------*/
             
        }
        else
        {

            /*------------------------
             *  se siamo stati visti dal nemico
             * -----------------------*/


            //qua mettere animazione di puntamento

            if (!setPlayerContact){
                CharacterControllerScript.player_contact = true;
                setPlayerContact = true;
            }

            navMesh.destination = player.transform.position;
            transform.LookAt(player.transform.position + (new Vector3(0, 1f, 0)));
            navMesh.stoppingDistance = 7;
            navMesh.speed = 2;
            if (navMesh.remainingDistance < 50)
            {
                //se è abbastanza vicino spara
                fireOnPlayer();

            }
        }

        if (headHit)
        {
            blood.Play();
            if (life > 0)
            {
                decrLife(6);
                hitSound.Play();
                if (!CharacterControllerScript.boss_contact)
                {
                    CharacterControllerScript.boss_contact = true;
                }
            }
            headHit = false;

        }

        if (bodyHit)
        {
            bloodBody.Play();
            if (life > 0)
            {
                decrLife(2);
                hitSound.Play();
                if(!CharacterControllerScript.boss_contact){
                    CharacterControllerScript.boss_contact = true;
                }
            }
            bodyHit = false;

        }

    }

    void fireOnPlayer()
    {
        RaycastHit hit;
        Vector3 fucile = navMesh.transform.position;
        fucile.y += 0.3f;
        if(nColpi>0){
            if (!isFiring && CharacterControllerScript.player_contact)
            {
                isFiring = true;
                fireTimer = Random.Range(0, 2);
                if (Physics.Raycast(fucile, navMesh.transform.forward, out hit))
                {
                    //animBoss.SetBool("isShooting", true);
                    //fire.enableEmission = true;
                    fire.Play();
                    bossFireSound.Play();
                    BossRecoil.recoilActive = true;

                    nColpi--;

                    Debug.Log("Enemy Fire");
                    Debug.DrawRay(fucile, navMesh.transform.forward * 10, Color.green);
                    Debug.Log("Nemico colpisce: " + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if (!CharacterControllerScript.immortality)
                        {
                            CharacterControllerScript.decrHealth(8);
                            if (CharacterControllerScript.isDead)
                            {
                                ShowMessage.id = 10;
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
        } else {
            //reload
            StartCoroutine(OnAnimationComplete());
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
        BossLife.show = false;
        CharacterControllerScript.boss_contact = false;
        ShowMessage.id = 0;
        speed = 0;
        animBoss.SetBool("isDead", true);
        Destroy(navMesh);
        navMesh.enabled = false;
        if (!isDead)
        {
            isDead = true;
            killOk = true;
        }
        Destroy(this);

    }

    IEnumerator OnAnimationComplete()
    {
        float reload_waittime = 2.4f;

        animBoss.SetBool("isReloading", true);

        reloadSound.Play();        //reload Sound
        navMesh.speed = 0;
        yield return new WaitForSeconds(reload_waittime);
        animBoss.SetBool("isReloading", false);
        navMesh.speed = 2;
        nColpi = nColpiStart;
        yield return true;
    }



}
