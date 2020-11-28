using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public AudioSource hitSound;
    private void OnTriggerEnter(Collider other)
    {
        DestroyObject(other.gameObject);
    }
}
