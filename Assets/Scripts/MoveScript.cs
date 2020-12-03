using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float objectSpeed = -0.3f;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            objectSpeed = objectSpeed * 0.1f;
        else
            objectSpeed = objectSpeed * 1f;
    }

    void Update()
    {
        

        transform.position = transform.position += new Vector3(0, 0, objectSpeed * Time.timeScale);
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
