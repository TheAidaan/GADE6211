using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<BossManager>().BossActivation();
       
    }
}
