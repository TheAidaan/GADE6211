using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitioner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterReact>() != null)
        {
            other.GetComponent<CharacterReact>().Transition();

            FindObjectOfType<GameManager>().Transition();
        }

    }
}
