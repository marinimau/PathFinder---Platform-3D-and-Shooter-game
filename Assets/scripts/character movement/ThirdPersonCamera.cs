using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public Camera camera;
    public float mouseSensitivity = 10;
    public Transform target;
    public Transform target_aim;
    public float distanceFromTarget = 2.0f;
    public float distanceFromTargetInAiming = 1.0f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation = Vector3.zero;

    Vector3 currentVelocity;


    float yaw;
    float pitch;
    bool flag_mira = false;

    private void Start()
    {
        camera = GetComponent<Camera>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    private void Update()
    {
        if(!PauseMenu.isPaused){
            if (!lockCursor)
            {
                Start();
                CharacterControllerScript.reset = true;

            }
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!PauseMenu.isPaused){
            if (Input.GetButton("Fire2"))
            {
                flag_mira = true;
                currentRotation = target_aim.eulerAngles;
                transform.eulerAngles = currentRotation;
                transform.position = Vector3.SmoothDamp(transform.position, target_aim.position - transform.forward * distanceFromTargetInAiming, ref currentVelocity, rotationSmoothTime);
            }

            if (Input.GetButtonUp("Fire2"))
            {
                if (flag_mira == true)
                {
                    yaw = transform.forward.x;
                    pitch = transform.forward.y;
                    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                    flag_mira = false;
                }

            }

            if (flag_mira == false)
            {
                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw, 0f), ref rotationSmoothVelocity, rotationSmoothTime);
                transform.eulerAngles = currentRotation;
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                transform.position = target.position - transform.forward * distanceFromTarget;


            }
        }

        else{
            /*se siamo in pausa*/
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                lockCursor = false;
            }

        }


    }
        
}
