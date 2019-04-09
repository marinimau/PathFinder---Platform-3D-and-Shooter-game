using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject pistol;
    public Animator anim;
    public AudioSource gunfire;
    public GameObject bulletTex;
    public int nColpi = 15;

    private void Start()
    {
        
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") & nColpi>0)
        {
            Shoot();
        } else {
            Click();
        }

        if(Input.GetKey(KeyCode.R)){
            Reload();
        }
    }

    void Shoot()
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
        nColpi--;
        Debug.Log("Numero colpi: "+nColpi);

    }

    void Click(){
        //riproduci suono arma scarica
        Debug.Log("arma scarica");
    }

    void Reload(){
        //animazione reload

        if(nColpi==0){
            //ricarico a arma scarica
            nColpi = 14;
            Debug.Log("reload a arma scarica");
        } else {
            //ricarico con un colpo in canna
            nColpi = 15;
            Debug.Log("reload con un colpo ancora in canna");
        }
    }

}
