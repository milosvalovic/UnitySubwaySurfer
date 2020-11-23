using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    //Variables
    public float speed = 2.0f;
    public float jumpSpeed = 20.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float currentX;
    private float nextX;
    private float direction;
    private bool didJump = false;
    CharacterController controller;


    private void Start()
    {
        currentX = transform.position.x;
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        
        currentX = transform.position.x;

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (currentX > 1.8f)
            {
                nextX = 0;
            }
            else if (currentX < 0.25f && currentX > -0.25f)
            {
                nextX = -2;
            }

            direction = -1;


        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (currentX < -1.8f)
            {
                nextX = 0;
            }
            else if (currentX < 0.25f && currentX > -0.25f)
            {
                nextX = 2;
            }
            direction = 1;


        }

        moveDirection = new Vector3(0, moveDirection.y, 0);
        if (direction == 1)
        {
            if (currentX < 0)
            {
                if (nextX != Mathf.Round(currentX - 0.5f))
                {
                    moveDirection = new Vector3(direction, 0, 0);
                    moveDirection = transform.TransformDirection(moveDirection);
                    //Multiply it by speed.
                    moveDirection *= speed;
                }
            }
            else
            {
                if (nextX != Mathf.Round(currentX - 0.5f))
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
                if (nextX != Mathf.Round(currentX + 0.5f))
                {
                    moveDirection = new Vector3(direction, 0, 0);
                    moveDirection = transform.TransformDirection(moveDirection);
                    //Multiply it by speed.
                    moveDirection *= speed;
                }
            }
            else
            {
                if (nextX != Mathf.Round(currentX + 0.5f))
                {
                    moveDirection = new Vector3(direction, 0, 0);
                    moveDirection = transform.TransformDirection(moveDirection);
                    //Multiply it by speed.
                    moveDirection *= speed;
                }
            }
        }

        if (didJump)
        {
            
            moveDirection.y = jumpSpeed ;
            didJump = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 0.06f;
            controller.center = new Vector3(0, 0.09f, -0.01f);
            GetComponent<Animator>().SetBool("Crouch", true);
            
        }

        //Making the character move


        //Applying gravity to the controller

    }

    void Update()
    {
        
        if (transform.position.y < 0.1f)
        {
            //Jumping
            if (Input.GetButton("Jump"))
                didJump = true;

            

        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Flip"))
        {
            GetComponent<Animator>().SetBool("Crouch", false);
            controller.height = 0.24f;
            controller.center = new Vector3(0, 0.125f, -0.01f);

        }

        Debug.Log(controller.isGrounded.ToString());
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        
        //moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        //controller.Move(moveDirection * Time.deltaTime);
        //Applying gravity to the controller
        //moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
