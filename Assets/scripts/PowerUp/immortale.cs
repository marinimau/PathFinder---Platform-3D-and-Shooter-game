using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class immortale : MonoBehaviour
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
            CharacterControllerScript.immortality = true;
            CharacterControllerScript.immortalityTimer = 1;
            c.active = false;
            Debug.Log("powerUp");
            //Destroy(gameObject);
            ShowMessage.id = 3;
            Talk.id = 5;
        }

    }
}
