using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuale_Ravvicinata : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "thug1" && !EnemySight.player_contact){
            EnemySight.player_contact = true;
            Show_stealth_status.icon = 2;
            //è per forza nella stessa luce che interseca il cono perchè il controllo lo facciamo quando il player è già nel cono
            Debug.Log(" player dentro il cono piccolo");
        }

    }

}
