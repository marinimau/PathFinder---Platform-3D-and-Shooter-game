using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseCamera : MonoBehaviour
{
    public Camera camPlayer;
    public Camera camMenu;

    // Start is called before the first frame update
    void Start()
    {
        camPlayer.enabled = true;
        camMenu.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            camPlayer.enabled = true;
            camMenu.enabled = false;
        }
        else{
            camPlayer.enabled = false;
            camMenu.enabled = true;
        }
    }
}
