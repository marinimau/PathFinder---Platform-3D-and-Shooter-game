using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public static bool area_illuminata;
    // Start is called before the first frame update
    void Start()
    {
        area_illuminata = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.player_contact)
        {
            Show_stealth_status.icon = 1;
            Debug.Log("entrato nell'area luminosa");
            area_illuminata = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.player_contact)
        {
            Show_stealth_status.icon = 0;
            area_illuminata = false;
            Debug.Log("uscito dall'area");
        }

    }
}
