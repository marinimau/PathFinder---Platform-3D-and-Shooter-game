using UnityEngine;
using System.Collections;

public class CompleteMovement : MonoBehaviour
{

    public float speed = 6.0f;
    public float airSpeed = 3.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float wallMultiplier = 0.75F;
    public float wallJumpSpeed = 90.0F;
    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    //bool isJumping;
    [SerializeField]
    int jumpLimit = 1; // New vairable
    int dJumpCounter = 0;     // New variable
    float currentWallMultiplier = 1.0F;
    private bool wallCollision = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {


        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }
        if (!controller.isGrounded)
        {
            moveDirection.x *= airSpeed;
            moveDirection.z *= airSpeed;
        }

        if (Input.GetButtonDown("Jump") && wallCollision == false)
        {
            if (controller.isGrounded)
            {
                moveDirection.y = jumpSpeed;
                dJumpCounter = 0;
            }
            if (!controller.isGrounded && dJumpCounter < (jumpLimit - 1))
            {
                moveDirection.y = jumpSpeed;
                dJumpCounter++;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime * currentWallMultiplier;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y < 0.1f && !controller.isGrounded)
        {
            currentWallMultiplier = wallMultiplier;
            dJumpCounter = 0;
            wallCollision = true;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection = hit.normal * speed * wallJumpSpeed;
                moveDirection.y = jumpSpeed;
                print("walljump");
            }
        }
        else
        {
            currentWallMultiplier = 1.0F;
            wallCollision = false;
        }
    }
}
