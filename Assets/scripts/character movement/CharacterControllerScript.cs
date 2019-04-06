using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float WaitTime = 0.5f;

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float global_target_rotation = 0.0f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    public float speedSmoothVelocity;
    public float currentSpeed;
    public float velocityY;

    public int mouseSensitivity = 10;

    public float airTime;
    public bool isJumping;
    public float staticJumpBuff;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float lastRotation; //Serve a resettare la posizione del personaggio durante la mira sull'asse verticale
    public Boolean flag = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        bool running = Input.GetKey(KeyCode.LeftShift);

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (Input.GetButton("Fire2"))
        {
            if (Input.GetAxis("Jump") > 0 && currentSpeed <= 0.1f)
            {
                animator.SetBool("jumpStatic", true);
                StartCoroutine("Jump_Static_Land", WaitTime);

            }
            else
            {
                if (Input.GetAxis("Jump") > 0)
                {
                    Jump();
                    animator.SetBool("Jumping", true);

                }
            }

            MoveWhileAiming(inputDir, running);

        }
        else
        {
            if(flag){
                transform.localEulerAngles = new Vector3(0, lastRotation, 0);
                flag = false;
            }

            if (Input.GetAxis("Jump") > 0 && currentSpeed <= 0.1f)
            {
                animator.SetBool("jumpStatic", true);
                StartCoroutine("Jump_Static_Land", WaitTime);

            }
            else
            {
                if (Input.GetAxis("Jump") > 0)
                {
                    Jump();
                    animator.SetBool("Jumping", true);

                }
            }

            //input per movimento
            Move(inputDir, running);


        }
        //animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("speedPercentage", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            global_target_rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;     //Se stiamo correndo allora la velocità sarà uguale a runspeed, altrimenti a walkspeed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));       //solo asse x e z

        velocityY += Time.deltaTime * gravity;      //velocità asse y calcolata a parte.

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            animator.SetBool("Jumping", false);

            velocityY = 0;
            airTime = 0;
            animator.SetBool("airTime", false);
            isJumping = false;
        }

        if (!controller.isGrounded)
        {
            airTime += Time.deltaTime;

            if (airTime > 1.2f && isJumping == true)
            {
                animator.SetBool("airTime", true);
            }

            if (airTime > 0.3f && isJumping == false)
            {
                animator.SetBool("airTime", true);
            }

        }

    }


    void MoveWhileAiming(Vector2 inputDir, bool running)
    {


        Vector2 inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxisRaw("Mouse Y"));

       //float max = 0, min = 0;

        if (inputMouse.x != 0)
        {

            //rotazione sull'asse y
            transform.Rotate(new Vector3(0, inputMouse.x * mouseSensitivity, 0), Space.World);
            lastRotation = transform.eulerAngles.y;
            flag = true;


        }
        
        if(inputMouse.y != 0 && (true)){
            //rotazioni sull'asse x
            Debug.Log("Angolo"+transform.localEulerAngles.x* Mathf.Rad2Deg);
            transform.Rotate(new Vector3(-inputMouse.y*mouseSensitivity, 0, 0));
        }

        Vector3 moveDirection;

        velocityY += Time.deltaTime * gravity;
        moveDirection = new Vector3(inputDir.x, velocityY, inputDir.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * 3;


        controller.Move(moveDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            animator.SetBool("Jumping", false);

            velocityY = 0;
            airTime = 0;
            animator.SetBool("airTime", false);
            isJumping = false;
        }

        if (!controller.isGrounded)
        {
            airTime += Time.deltaTime;

            if (airTime > 1.2f && isJumping == true)
            {
                animator.SetBool("airTime", true);
            }

            if (airTime > 0 && isJumping == false)
            {
                animator.SetBool("airTime", true);
            }

        }
    }


    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            isJumping = true;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }

        return smoothTime / airControlPercent;
    }

    IEnumerator Jump_Static_Land(float Count)
    {
        yield return new WaitForSeconds(Count);
        Jump();
        yield return new WaitForSeconds(Count);
        animator.SetBool("jumpStatic", false);

        yield return null;
    }

}
