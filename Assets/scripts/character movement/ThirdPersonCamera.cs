using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 2.0f;
    public float distanceFromTargetInAiming = 2.0f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;


    float yaw;
    float pitch;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetButton("Fire2")){

            currentRotation = Vector3.SmoothDamp(target.eulerAngles,currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);
             transform.eulerAngles = currentRotation;

             transform.position=target.position - transform.forward * distanceFromTarget;


        


    } else {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;

        }
       
    }
}
