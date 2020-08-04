using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            FindObjectOfType<BossManager>().BossActivation();
            Destroy(gameObject);
        }  
       
    }
}
