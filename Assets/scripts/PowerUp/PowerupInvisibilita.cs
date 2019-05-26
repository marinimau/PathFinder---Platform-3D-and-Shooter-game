using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvisibilita : MonoBehaviour
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
            //riprodurre un suono che ci dia un feedback quando entriamo
            if(!CharacterControllerScript.invisible){;
                ShowMessage.id = 4;
                CharacterControllerScript.invisible = true;
                CharacterControllerScript.invisibleTimer = 1;
                c.active = false;
                Talk.id = 5;
            }
        }

    }
}
