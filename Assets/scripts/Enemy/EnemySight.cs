using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*---------------------------
 *  cono da lunga distanza, funziona solo con fonte luminosa
 * --------------------------*/
public class EnemySight : MonoBehaviour
{

    public static bool player_contact;
    public static bool player_contact_deactivated; //flag per ricreare i waypoint se il player riersce a sfiggire

    public Patrol enemy;

    // Start is called before the first frame update
    void Start()
    {
        player_contact = false;
        player_contact_deactivated = false;
        enemy = GetComponentInParent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_contact){
            Show_stealth_status.icon = 2;
        }

        if(enemy.isDead_enemy){
            Destroy(gameObject);
            Debug.Log("Cono grande distrutto");
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
        if (other.gameObject.tag.Equals("Player") && !player_contact)
        {
            player_contact = false;
            Debug.Log(" player_contact " + player_contact);

        }

    }

    private void controlloContatto(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && !player_contact && !CharacterControllerScript.invisible)
        {
            if (Show_stealth_status.icon == 0)
            {
                Debug.Log(" player dentro ma non è illuminato ");
            }
            else
            {
                player_contact = true;
                //è per forza nella stessa luce che interseca il cono perchè il controllo lo facciamo quando il player è già nel cono
                Debug.Log(" player dentro illuminato, player_contact: " + player_contact);
            }



        }
    }

}
