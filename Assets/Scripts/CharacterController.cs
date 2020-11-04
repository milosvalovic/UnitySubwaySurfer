using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float forceConst = 50;

    private bool canJump;
    private Rigidbody selfRigidbody;
    private bool inJump = false;

    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        

        

    }

    void Update()
    {
        if (canJump)
        {
            canJump = false;
            inJump = true;
            selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
            GetComponent<Animator>().SetBool("Jump", true);
        }
        if (inJump)
        {
            if (transform.position.y > 3)
            {
                selfRigidbody.AddForce(0, -forceConst * 1.2f, 0, ForceMode.Impulse);
                GetComponent<Animator>().SetBool("Jump", false);
                inJump = false;
            }
        }
        else
        {
            if (transform.position.y < 0.2)
            {
                transform.position = new Vector3(0, 0, 0);
                selfRigidbody.useGravity = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!canJump)
            canJump = true;
            
        }
        

    }
}
