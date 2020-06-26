using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsFinished : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.eulerAngles = new Vector3(0, 0, 0);
        other.GetComponentInParent<CharacterMovement>().StopForwardMovement(true, false);
        other.GetComponentInParent<CharacterMovement>().LockControls(true);
        GetComponentInParent<Boss_1_InsideTower>().PlayerIsInPlace(true);

        Destroy(gameObject);
    }
}
