using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag.Equals("Player"))
            {

                Talk.id = 5;
                CharacterControllerScript.key = true;
                Debug.Log("collectible");
                Destroy(gameObject);
            }

        
    }
}
