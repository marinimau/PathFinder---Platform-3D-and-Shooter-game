using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPiuRicercato : MonoBehaviour
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
        //player
        if (other.gameObject.tag.Equals("Player"))
        {
            //riprodurre un suono che ci dia un feedback quando entriamo
            EnemySight.player_contact = false;
            EnemySight.player_contact_deactivated = true;
            Show_stealth_status.icon = 0;
            Debug.Log("collectible");
            Destroy(gameObject);
            ShowMessage.id = 2;
        }

    }

}
