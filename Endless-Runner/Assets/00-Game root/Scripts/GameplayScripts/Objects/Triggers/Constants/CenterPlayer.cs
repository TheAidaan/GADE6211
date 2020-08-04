using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<CharacterMovement>() != null)
        {
            other.gameObject.GetComponentInParent<CharacterMovement>().CenterPlayer();
        }
        
    }
}
