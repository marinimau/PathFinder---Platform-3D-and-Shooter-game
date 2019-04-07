using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public Transform target_aim;
    public float distanceFromTarget = 2.0f;
    public float distanceFromTargetInAiming = 1.0f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    Vector3 currentVelocity;


    float yaw;
    float pitch;
    bool flag_mira = false;

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

        if (Input.GetButton("Fire2"))
        {
            flag_mira = !flag_mira;
            currentRotation = target_aim.localEulerAngles;
            transform.localEulerAngles = currentRotation;
            transform.position = Vector3.SmoothDamp(transform.position, target_aim.position - transform.forward * distanceFromTargetInAiming, ref currentVelocity, rotationSmoothTime);
            flag_mira = !flag_mira;
        }
        else
        {
            if(flag_mira == false)
            {
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            }
            else
            {
                yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                flag_mira = !flag_mira;
            }

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;


        }

    }
}
