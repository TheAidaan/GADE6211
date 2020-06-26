using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwayPowers: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        other.gameObject.GetComponent<CharacterReact>().EndFling();
    }
}
