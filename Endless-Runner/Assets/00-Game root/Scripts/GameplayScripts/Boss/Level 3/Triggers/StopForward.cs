using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponentInParent<CharacterMovement>().StopForwardMovement(true, false);
    }
}
