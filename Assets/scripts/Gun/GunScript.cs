using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject pistol;
    public Animator anim;
    public AudioSource gunfire;
    public GameObject bulletTex;

    private void Start()
    {
        
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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

    }

}
