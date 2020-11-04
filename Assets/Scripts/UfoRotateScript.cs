using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoRotateScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 80 * Time.deltaTime);
    }
}
