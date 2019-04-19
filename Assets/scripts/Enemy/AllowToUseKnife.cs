using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowToUseKnife : MonoBehaviour
{
    public GameObject enemy;
    bool allowKnife;
    // Start is called before the first frame update
    void Start()
    {
        allowKnife = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(allowKnife){
            //player può usare il coltello

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag.Equals("Player") && !CharacterControllerScript.isDead)
        {
            allowKnife = true;
            ShowMessage.id = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            allowKnife = false;
            ShowMessage.id = 0;
        }

    }
}
