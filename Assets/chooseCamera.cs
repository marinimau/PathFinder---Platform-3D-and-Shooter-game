using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseCamera : MonoBehaviour
{
    public Camera playerCamera;
    public ThirdPersonCamera cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = playerCamera.GetComponent<ThirdPersonCamera>();
        cameraScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused){
            cameraScript.enabled = true;
        } else {
            cameraScript.enabled = false;
        }
    }
}
