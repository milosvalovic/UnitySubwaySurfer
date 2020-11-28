using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float objectSpeed = -0.5f;

    void Update()
    {
        
        transform.position = transform.position += new Vector3(0, 0, objectSpeed);
    }


    public void StopMovement() {
        objectSpeed = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            if (GetComponent<GameObject>() != null) {
                Destroy(this);
            }
        }
    }
}
