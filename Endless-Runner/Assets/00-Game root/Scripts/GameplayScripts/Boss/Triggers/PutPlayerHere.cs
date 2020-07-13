using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlayerHere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.position = transform.position;
        Destroy(gameObject);
    }
}
