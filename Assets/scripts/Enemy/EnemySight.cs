using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*---------------------------
 *  cono da lunga distanza, funziona solo con fonte luminosa
 * --------------------------*/
public class EnemySight : MonoBehaviour
{

    public bool player_contact;
    public bool light;

    // Start is called before the first frame update
    void Start()
    {
        player_contact = false;
        light = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player_contact && light){
            Show_stealth_status.icon = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if(other.gameObject.name=="thug1"){
            player_contact = true;
            Debug.Log(" player_contact " + player_contact);
        }
        //luce
        if (other.gameObject.name == "light")
        {
            light = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "thug1")
        {
            player_contact = false;
            Debug.Log(" player_contact " + player_contact);

        }

        if (other.gameObject.name == "light")
        {
            light = false;
        }


    }


}
