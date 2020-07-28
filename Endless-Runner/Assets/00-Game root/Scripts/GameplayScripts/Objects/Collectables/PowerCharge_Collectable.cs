using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCharge_Collectable: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other?.GetComponent<CharacterStats>() ? .ChangePower(25f) ;
        Destroy(gameObject);
    }
}
