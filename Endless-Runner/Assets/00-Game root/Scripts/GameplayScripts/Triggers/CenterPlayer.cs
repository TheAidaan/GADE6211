using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponentInParent<CharacterMovement>().CenterPlayer();
        other.gameObject.GetComponent<CharacterReact>().EndSuperSize();
    }
}
