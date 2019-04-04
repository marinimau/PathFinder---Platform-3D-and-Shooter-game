using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject pistol;


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

        if (Physics.Raycast(pistol.transform.position, pistol.transform.right, out hit, range))
        {
            Debug.DrawRay(pistol.transform.position, pistol.transform.right * 10, Color.green);
            Debug.Log(hit.transform.name);
        }

    }
}
