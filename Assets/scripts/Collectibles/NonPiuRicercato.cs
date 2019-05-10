using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPiuRicercato : MonoBehaviour
{

    public ParticleSystem smoke;
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
            //riprodurre un suono che ci dia un feedback quando entriamo
            CharacterControllerScript.player_contact = false;
            CharacterControllerScript.player_contact_deactivated = true;
            Show_stealth_status.icon = 0;
            Debug.Log("collectible");
            smoke.Play();
            //Destroy(gameObject);
            c.active = false;
            ShowMessage.id = 2;
            Talk.id = 5;
        }

    }

}
