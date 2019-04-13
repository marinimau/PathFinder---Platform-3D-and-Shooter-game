﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowToUseKnife : MonoBehaviour
{

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
        if (other.gameObject.name == "thug1")
        {
            allowKnife = true;
            ShowMessage.id = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "thug1")
        {
            allowKnife = false;
            ShowMessage.id = 0;
        }

    }
}
