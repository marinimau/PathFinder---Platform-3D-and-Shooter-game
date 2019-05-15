using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialBullet : MonoBehaviour
{
    public collectible c;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<collectible>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && c.active)
        {
            if (!CharacterControllerScript.specialBullet)
            {
                ShowMessage.id = 11;
                CharacterControllerScript.specialBullet = true;
                CharacterControllerScript.specialBulletTimer = 1;
                c.active = false;
                Talk.id = 5;
            }
        }

    }
}
