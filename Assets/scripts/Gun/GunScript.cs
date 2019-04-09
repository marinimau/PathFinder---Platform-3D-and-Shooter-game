using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject pistol;
    public Animator anim;
    public AudioSource gunfire;
    public GameObject bulletTex;
    public static int nColpi = 14;
    public static bool armaScarica = false;


    private void Start()
    {
        
    }

    void Update()
    {
        if (nColpi < 0)
        {
            armaScarica = true;
        }

        if (Input.GetButtonDown("Fire1")){
            if (!armaScarica)
            {
                nColpi=Shoot();
            }
            else
            {
                Click();
            }

        }

        if(Input.GetKey(KeyCode.R)){
            Reload();
        }
    }

    int Shoot()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(pistol.transform.position, pistol.transform.forward, out hit, range))
        {
            Instantiate(bulletTex, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            gunfire.Play();
            Debug.DrawRay(pistol.transform.position, pistol.transform.forward * 10, Color.green);
            Debug.Log(hit.transform.name);
        }
        else
        {
            gunfire.Play();
            Debug.DrawRay(pistol.transform.position, pistol.transform.forward * 10, Color.green);
        }
        Debug.Log("Numero colpo: "+(nColpi));

        return nColpi - 1;

    }

    void Click(){
        //riproduci suono arma scarica
        Debug.Log("arma scarica");
    }

    void Reload(){
        //animazione reload

        if(armaScarica){
            //ricarico a arma scarica
            nColpi = 13;
            Debug.Log("reload a arma scarica");
            armaScarica = false;
        } else {
            //ricarico con un colpo in canna
            nColpi = 14;
            Debug.Log("reload con un colpo ancora in canna");
            armaScarica = false;
        }
    }

    public static bool getArmaScarica(){
        return armaScarica;
    }
}
