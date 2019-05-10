using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : MonoBehaviour
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
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && c.active)
        {
            CharacterControllerScript.incHealth(30);
            //Destroy(gameObject);
            Talk.id = 5;
            c.active = false;
        }
    }
}
