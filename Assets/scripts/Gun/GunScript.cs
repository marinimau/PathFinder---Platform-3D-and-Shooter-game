using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public GameObject pistol;
    public Animator anim;
    public Animator animHeadShot;
    public AudioSource gunfire;
    public AudioSource reload_sound;
    public GameObject bulletTex;
    public static int nColpi = 14;
    public static bool armaScarica;

    public GameObject player;
    Animation animation;
    CharacterControllerScript controller;
    public ParticleSystem bloodEffect;
    public GameObject light;
    private Patrol enemy;
    private Sniper sniper;
    private Boss boss;
    private Boris boris;

    private void Start()
    {
        armaScarica = false;
        anim = player.GetComponent<Animator>();
        controller = player.GetComponent<CharacterControllerScript>();
    }

    void Update()
    {
        if (nColpi < 0)
        {
            armaScarica = true;
        }

        if (Input.GetButtonDown("Fire1") && !CharacterControllerScript.isDead){
            if(!armaScarica){
                nColpi = Shoot();
            }
            else
            {
                Click();
            }
        }
       

        if (Input.GetKey(KeyCode.R) && !CharacterControllerScript.isDead)
        {
            StartCoroutine(OnAnimationComplete());

        }
    }

    int Shoot()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(pistol.transform.position, pistol.transform.forward, out hit, range))
        {
            gunfire.Play();
            Debug.DrawRay(pistol.transform.position, pistol.transform.forward * 10, Color.green);
            animHeadShot = hit.transform.GetComponentInParent<Animator>();

            Behaviour navMesh = hit.transform.GetComponentInParent<Behaviour>();

            //light point
            if (hit.transform.tag.Equals("LightPoint"))
            {
                Debug.Log("colpito punto luce");
                light = hit.collider.gameObject;
                Destroy(light);
            }

            if (hit.transform.tag.Equals("Head"))   //Se viene colpito un nemico in testa
            {
                enemy = hit.transform.GetComponentInParent<Patrol>();
                navMesh.enabled = false;
                animHeadShot.SetBool("isHeadHit", true);
                Talk.id = 4;
                Debug.Log("HEADSHOT!");
            }
            else if (hit.transform.tag.Equals("Body"))  //Se non viene colpito un nemico nel corpo
            {
                enemy = hit.transform.GetComponentInParent<Patrol>();
                Debug.Log("Hai colpito il corpo");
                enemy.bodyHit = true;

            }
            else if (hit.transform.tag.Equals("HeadSniper"))   //Se viene colpito un nemico in testa
            {
                sniper = hit.transform.GetComponentInParent<Sniper>();
                navMesh.enabled = false;
                sniper.headHit = true;
                Talk.id = 4;
                Debug.Log("HEADSHOT!");
            }
            else if (hit.transform.tag.Equals("BodySniper"))  //Se non viene colpito un nemico nel corpo
            {
                sniper = hit.transform.GetComponentInParent<Sniper>();
                Debug.Log("Hai colpito il corpo");
                sniper.bodyHit = true;

            }
            else if (hit.transform.tag.Equals("HeadBoss"))   //Se viene colpito un nemico in testa
            {
                boss = hit.transform.GetComponentInParent<Boss>();
                navMesh.enabled = false;
                boss.headHit = true;
                Debug.Log("HEADSHOT!");
            }
            else if (hit.transform.tag.Equals("BodyBoss"))  //Se non viene colpito un nemico nel corpo
            {
                boss = hit.transform.GetComponentInParent<Boss>();
                Debug.Log("Hai colpito il corpo");
                boss.bodyHit = true;

            }
            else if (hit.transform.tag.Equals("HeadBoris"))   //Se viene colpito un nemico in testa
            {
                boris = hit.transform.GetComponentInParent<Boris>();
                navMesh.enabled = false;
                boris.headHit = true;
                Debug.Log("HEADSHOT!");
            }
            else if (hit.transform.tag.Equals("BodyBoris"))  //Se non viene colpito un nemico nel corpo
            {
                boris = hit.transform.GetComponentInParent<Boris>();
                Debug.Log("Hai colpito il corpo");
                boris.bodyHit = true;

            }
            else    //Se non colpisci un nemico
            {
                Physics.IgnoreCollision(hit.collider, hit.collider);
                GameObject clone = Instantiate(bulletTex, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(clone, 10f);
            }

        }
        else
        {
            gunfire.Play();
            Debug.DrawRay(pistol.transform.position, pistol.transform.forward * 10, Color.green);
        }
        Debug.Log("Numero colpo: " + (nColpi));

        return nColpi - 1;

    }

    void Click()
    {
        //riproduci suono arma scarica
        Debug.Log("arma scarica");
        Talk.id = 3;
    }

    void Reload()
    {

        Talk.id = 0;
        if (armaScarica)
        {
            //ricarico a arma scarica
            nColpi = 13;
            Debug.Log("reload a arma scarica");
            armaScarica = false;
        }
        else
        {
            //ricarico con un colpo in canna
            nColpi = 14;
            Debug.Log("reload con un colpo ancora in canna");
            armaScarica = false;
        }
    }

    public static bool getArmaScarica()
    {
        return armaScarica;
    }

    IEnumerator OnAnimationComplete()
    {
        float reload_waittime = 2.0f;
        armaScarica = true;
        anim.SetBool("reloading", true);
        reload_sound.Play();        //reload Sound
        controller.setReloadingWalkSpeed();
        yield return new WaitForSeconds(reload_waittime);
        anim.SetBool("reloading", false);
        controller.setStandardWalkSpeed();
        Reload();
        yield return true;
    }

}
