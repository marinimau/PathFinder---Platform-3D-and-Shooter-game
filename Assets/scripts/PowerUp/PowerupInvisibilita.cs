using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvisibilita : MonoBehaviour
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
            if(!CharacterControllerScript.invisible){;
                ShowMessage.id = 4;
                CharacterControllerScript.invisible = true;
                CharacterControllerScript.invisibleTimer = 1;
                gameObject.SetActive(false);
                Talk.id = 5;
            }
        }

    }
}
