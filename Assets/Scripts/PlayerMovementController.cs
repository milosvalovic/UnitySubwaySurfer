using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //Variables
    public float speed = 8.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float currentX;
    private float nextX;
    private float direction;


    private void Start()
    {
        currentX = transform.position.x;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        currentX = transform.position.x;
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.

            if (Input.GetAxis("Horizontal") < 0) {
                if (currentX > 1.8f)
                {
                    nextX = 0;
                }
                else if (currentX < 0.1f && currentX > -0.1f) {
                    nextX = -2;
                }
                
                direction = -1;
                
            } else if (Input.GetAxis("Horizontal") > 0)
            {
                if (currentX < -1.8f)
                {
                    nextX = 0;
                }
                else if (currentX < 0.1f && currentX > -0.1f)
                {
                    nextX = 2;
                } 
                direction = 1;

            }
            moveDirection = new Vector3(0, 0, 0);
            if (direction == 1)
            {
                if (currentX < 0)
                {
                    if (nextX + currentX <= 0.01f)
                    {
                        moveDirection = new Vector3(direction, 0, 0);
                        moveDirection = transform.TransformDirection(moveDirection);
                        //Multiply it by speed.
                        moveDirection *= speed;
                    }
                }
                else {
                    if (-nextX + currentX <= 0.01f)
                    {
                        moveDirection = new Vector3(direction, 0, 0);
                        moveDirection = transform.TransformDirection(moveDirection);
                        //Multiply it by speed.
                        moveDirection *= speed;
                    }
                }
            }
            else
            {
                if (currentX < 0)
                {
                    if (nextX - currentX <= 0.01)
                    {
                        moveDirection = new Vector3(direction, 0, 0);
                        moveDirection = transform.TransformDirection(moveDirection);
                        //Multiply it by speed.
                        moveDirection *= speed;
                    }
                }
                else {
                    if (-nextX - currentX <= 0.01f)
                    {
                        moveDirection = new Vector3(direction, 0, 0);
                        moveDirection = transform.TransformDirection(moveDirection);
                        //Multiply it by speed.
                        moveDirection *= speed;
                    }
                }
            }
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
