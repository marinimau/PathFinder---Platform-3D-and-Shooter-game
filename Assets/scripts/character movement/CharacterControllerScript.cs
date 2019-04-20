using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    
    public float WaitTime = 0.5f;
    float standard_walkspeed = 2;
    float reloading_walkspeed = 0.25f;
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    public float timerJump = 0;
    public float timeRientroRinculo=2f;
    public float smooth;
    Quaternion old_rotation;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    public int mouseSensitivity = 10;

    float airTime;
    bool isJumping;
    float staticJumpBuff;

    public static int health;
    public static bool isDead;
    public static bool immortality;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    public Vector2 pitchMinMax = new Vector2(-40, 85);
    float lastRotation; //Serve a resettare la posizione del personaggio durante la mira sull'asse verticale
    public Boolean flag = false;

    float targetSpeed;
    public static Boolean fire = false;



    // Start is called before the first frame update
    void Start()
    {
        old_rotation = transform.rotation;
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        health = 100;
        isDead = false;
        immortality = false;
    }

    // Update is called once per frame
    void Update()
    {

        bool running = Input.GetKey(KeyCode.LeftShift);

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        if(!isDead){
            if (Input.GetButton("Fire2"))
            {
                if (Input.GetAxis("Jump") > 0 && currentSpeed <= 0.1f && !isJumping)
                {
                    animator.SetBool("jumpStatic", true);
                    StartCoroutine("Jump_Static_Land", WaitTime);

                }
                else
                {
                    if (Input.GetAxis("Jump") > 0)
                    {
                        JumpWhileAiming();
                        animator.SetBool("Jumping", true);

                    }
                }
                MoveWhileAiming(inputDir, running);
            }
            else
            {

                if (flag)
                {
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


            if (Input.GetButtonDown("Fire1") && !fire && !GunScript.armaScarica)
            {
                //AnimazioneSparo();
                Recoil.recoilActive = true;
            }

            if (fire)
            {
                smooth += Time.deltaTime * 4F;
                transform.rotation = Quaternion.Lerp(transform.rotation, old_rotation, smooth);
                if (smooth > 1)
                {
                    fire = false;
                }
            }


            //animator
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            animator.SetFloat("speedPercentage", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        } else{
            //Il player è morto
            Debug.Log("Player Ucciso");
        }

    }

    void AnimazioneSparo(){
        if(!fire){
            smooth = 0;
            old_rotation = transform.rotation;
            transform.Rotate(new Vector3(-5, 0, 0));
            fire = true;
        }
    }



    void Move(Vector2 inputDir, bool running)
    {
        gravity = -12;
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;     //Se stiamo correndo allora la velocità sarà uguale a runspeed, altrimenti a walkspeed;
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

        animator.SetBool("airTime", false);

        gravity = controller.isGrounded && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Vertical") <= 0.5f ? -1f : -12f;

        Vector2 inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //float max = 0, min = 0;
        if (inputMouse.x != 0)
        {
            //rotazione sull'asse y
            transform.Rotate(new Vector3(0, inputMouse.x * mouseSensitivity, 0), Space.World);
            lastRotation = transform.eulerAngles.y;
            flag = true;
        }

        if (inputMouse.y != 0 && ((transform.eulerAngles.x - inputMouse.y * mouseSensitivity) >= float.MinValue
            && transform.eulerAngles.x - inputMouse.y * mouseSensitivity < 45)
            || (transform.eulerAngles.x - inputMouse.y * mouseSensitivity >= 320
            && transform.eulerAngles.x - inputMouse.y * mouseSensitivity <= float.MaxValue))
        {
            //rotazioni sull'asse x
            transform.Rotate(new Vector3(-inputMouse.y * mouseSensitivity, 0, 0));

        }

        Vector3 moveDirection;
        targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;     //Se stiamo correndo allora la velocità sarà uguale a runspeed, altrimenti a walkspeed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));       //solo asse x e z
        velocityY += Time.deltaTime * gravity;

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        moveDirection = new Vector3(inputDir.x, velocityY, inputDir.y);

        // Debug.Log(" ");
        moveDirection = transform.TransformDirection(moveDirection);

        if (running == false)
        {
            moveDirection = moveDirection * walkSpeed;
        }
        else
        {
            moveDirection = moveDirection * runSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);

        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            animator.SetBool("Jumping", false);

            velocityY = 0;
            airTime = 0;
            //animator.SetBool("airTime", false);
            isJumping = false;
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


    void JumpWhileAiming()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity/2;
            isJumping = true;
            timerJump = 0.3f;
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
        JumpWhileAiming();
        yield return new WaitForSeconds(Count);
        animator.SetBool("jumpStatic", false);

        yield return null;
    }

    public void setReloadingWalkSpeed()
    {
        this.walkSpeed = reloading_walkspeed;
    }

    public void setStandardWalkSpeed()
    {
        this.walkSpeed = standard_walkspeed;
        animator.SetFloat("speedPercentage", 0f);
    }

    public void standStill()
    {
        this.walkSpeed = 0.0f;
    }

    public static void decrHealth(int damage){
        if(health-damage>0){
            health -= damage;
        } else{
            isDead = true;
        }

    }

    public static void incHealth(int cura)
    {
        if (health + cura <=100)
        {
            health+= cura;
        }
        else
        {
            health = 100;
        }
    }
}
