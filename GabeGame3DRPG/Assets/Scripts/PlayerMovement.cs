using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public Transform model;

    public Transform cam;

    public float speed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public CharacterController controller;

    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    bool canJump;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canJump = true;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (mouseLook.thirdPerson)
        {
            Vector3 direction = new Vector3(x, 0f, z).normalized;

            if(direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }

        }
        else
        {
            Vector3 move = transform.right * x + transform.forward * z;
            model.position = gameObject.transform.position;

            controller.Move(move * speed * Time.deltaTime);
        }

        

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        if (x > 0 || z != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (animator.GetBool("isWalking") && Input.GetButton("Run"))
        {
            animator.SetBool("isRunning", true);
            speed = 6f;
            model.position = gameObject.transform.position;
        }
        else
        {
            animator.SetBool("isRunning", false);
            speed = 3f;
            model.position = gameObject.transform.position;
        }



        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
