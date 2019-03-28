using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public float speed = 1f;
    public float turnSpeed = 1f;


    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {

        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        float turnVelocity = turnSpeed * x;

        Vector3 rotationToApply = new Vector3(0, turnVelocity, 0);
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + rotationToApply);


        if (y != 0f)
        { //Stato Locomotion
            animator.SetBool("isMoving", true);
            
            float velocity = speed * y;
            float yVelocity = rb.velocity.y;
            rb.velocity = transform.forward * velocity + new Vector3(0, yVelocity, 0);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        animator.SetFloat("Velocity", y);
        animator.SetFloat("turnRate", x);
    }


}
