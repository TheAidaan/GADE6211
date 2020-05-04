using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : World{


    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponentInParent<CharacterMovement>().CenterPlayer();
        other.gameObject.GetComponent<CharacterReact>().EndSuperSize();
    }
}
