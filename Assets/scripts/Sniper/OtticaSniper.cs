using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*---------------------------
 *  cono da lunga distanza, funziona solo con fonte luminosa
 * --------------------------*/
public class OtticaSniper : MonoBehaviour
{


    public Sniper sniper;

    // Start is called before the first frame update
    void Start()
    {
        CharacterControllerScript.player_contact = false;
        CharacterControllerScript.player_contact_deactivated = false;
        sniper = GetComponentInParent<Sniper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControllerScript.player_contact)
        {
            Show_stealth_status.icon = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        controlloContatto(other);

    }

    private void OnTriggerStay(Collider other)
    {
        controlloContatto(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.player_contact)
        {
            CharacterControllerScript.player_contact = false;
            Debug.Log(" player_contact " + CharacterControllerScript.player_contact);

        }

    }

    private void controlloContatto(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.gameOver && !sniper.isDead && !CharacterControllerScript.player_contact && !CharacterControllerScript.invisible)
        {
            if (Show_stealth_status.icon == 0)
            {
                Debug.Log(" player dentro ma non è illuminato ");
            }
            else
            {
                CharacterControllerScript.player_contact = true;
                //è per forza nella stessa luce che interseca il cono perchè il controllo lo facciamo quando il player è già nel cono
                Debug.Log(" player dentro illuminato, player_contact: " + CharacterControllerScript.player_contact);
            }



        }
    }

}
