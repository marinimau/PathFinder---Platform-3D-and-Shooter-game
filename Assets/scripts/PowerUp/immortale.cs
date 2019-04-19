using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class immortale : MonoBehaviour
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
            CharacterControllerScript.immortality = true;
            Debug.Log("powerUp");
            Destroy(gameObject);
            ShowMessage.id = 3;
        }

    }
}
