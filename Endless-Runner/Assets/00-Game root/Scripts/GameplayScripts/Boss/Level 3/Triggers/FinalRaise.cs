using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRaise : MonoBehaviour
{
    
    Rigidbody playerRb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>() != null)
        {
            playerRb = other.GetComponentInParent<Rigidbody>();
            playerRb.AddForce(Vector3.up * 750, ForceMode.Impulse);
        }

        if (other.gameObject.transform.parent.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
