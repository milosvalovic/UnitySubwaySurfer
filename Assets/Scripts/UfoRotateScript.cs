using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoRotateScript : MonoBehaviour
{

    public bool floating = false;
    public float amplitude = 5;          //Set in Inspector 
    public float speed = 5;                  //Set in Inspector 
    private float tempVal;
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        tempVal = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (floating) {
            tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
            transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
        }
        transform.RotateAround(transform.position, Vector3.up, 80 * Time.deltaTime);
    }
}
