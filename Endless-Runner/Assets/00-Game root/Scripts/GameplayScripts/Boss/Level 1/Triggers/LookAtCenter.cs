using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Quaternion _lookRotation =
         Quaternion.LookRotation((transform.parent.position - other.transform.parent.position).normalized);
        other.transform.parent.rotation = _lookRotation;
    }
}
