using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    //Variables
    public float speed = 2.0f;
    public float jumpSpeed = 25.0f;
    public float gravity = 12.5f;
    public float flipTime = .5f;
    public float flipTimeDifference = 0.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float currentX;
    private float nextX;
    private float direction;
    private bool didJump = false;
    private bool didCrouch = false;
    CharacterController controller;
    public AudioSource footsteps;
    public AudioSource hitSound;
    public AudioSource powerupSound;
    public AudioMixerGroup mixerGroup;
    
    private float playerMoveDirection = 0;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public Text score;
    

    public float SWIPE_THRESHOLD = 200f;


    private void Start()
    {
        hitSound.outputAudioMixerGroup = mixerGroup;
        footsteps.outputAudioMixerGroup = mixerGroup;
        powerupSound.outputAudioMixerGroup = mixerGroup;

        currentX = transform.position.x;
        controller = GetComponent<CharacterController>();
        footsteps.Play();
    }

    void FixedUpdate()
    {
        
        currentX = transform.position.x;       


        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }

        

        playerMoveDirection += Input.GetAxis("Horizontal");

        if (playerMoveDirection < 0)
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

            playerMoveDirection = 0;
        }
        else if (playerMoveDirection > 0)
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

            playerMoveDirection = 0;

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


        if (didCrouch)
        {
            controller.height = 0.06f;
            controller.center = new Vector3(0, 0.09f, -0.01f);
            GetComponent<Animator>().SetBool("Crouch", true);
            didCrouch = false;
            flipTimeDifference = 0;
       
        }

        flipTimeDifference += Time.deltaTime;
        if (flipTimeDifference > flipTime) {
            GetComponent<Animator>().SetBool("Crouch", false);
            
            controller.height = 0.24f;
            controller.center = new Vector3(0, 0.125f, -0.01f);
        }

        //Making the character move


        //Applying gravity to the controller

    }

    void Update()
    {
        if (Time.timeScale == 0)
            footsteps.Stop();
        else
            footsteps.Play();



        if (transform.position.y < 0.1f)
        {

            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
            //Jumping
            if (Input.GetButton("Jump"))
            {
                didJump = true;
                footsteps.Stop();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                didCrouch = true;
            }

        }


        Debug.Log(controller.isGrounded.ToString());
        moveDirection.y -= gravity * Time.deltaTime * 2;
        controller.Move(moveDirection * Time.deltaTime);

        //Korekcia
        if (transform.position.y < -0.2f)
        {
            transform.position = new Vector3(0, 0.7f, 0);
        }
        if (transform.position.x < -2.5f)
        {
            transform.position = new Vector3(-2, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 2.5f)
        {
            transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            DestroyObject(other.gameObject);
            hitSound.Play();
            Time.timeScale = 0;
        }

        if ((other.tag == "PowerUP")) {
            powerupSound.Play();
            DestroyObject(other.gameObject);
            score.text = (int.Parse(score.text) + 1) + "";
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - (fingerUp.y-10) < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        if(transform.position.y < 0.1f)
            didJump = true;
    }

    void OnSwipeDown()
    {
        didCrouch = true;
    }

    void OnSwipeLeft()
    {
        playerMoveDirection = -0.5f;
    }

    void OnSwipeRight()
    {
        playerMoveDirection = 0.5f;
    }


}
