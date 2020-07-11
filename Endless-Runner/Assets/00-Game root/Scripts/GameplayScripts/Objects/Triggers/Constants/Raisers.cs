using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raisers : MonoBehaviour
{
    Rigidbody playerRb;
    private void OnTriggerEnter(Collider other)
    {
        playerRb = other.GetComponentInParent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 350, ForceMode.Impulse);
    }
}
