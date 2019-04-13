using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*---------------------------
 *  cono da lunga distanza, funziona solo con fonte luminosa
 * --------------------------*/
public class EnemySight : MonoBehaviour
{

    public static bool player_contact;

    // Start is called before the first frame update
    void Start()
    {
        player_contact = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player_contact){
            Show_stealth_status.icon = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if(other.gameObject.name=="thug1" && !player_contact)
        {
            if(Show_stealth_status.icon==0){
                Debug.Log(" player dentro ma non è illuminato ");
            } else {
                player_contact = true;
                //è per forza nella stessa luce che interseca il cono perchè il controllo lo facciamo quando il player è già nel cono
                Debug.Log(" player dentro illuminato, player_contact: " + player_contact);
            }


        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "thug1" && !player_contact)
        {
            player_contact = false;
            Debug.Log(" player_contact " + player_contact);

        }

    }


}
