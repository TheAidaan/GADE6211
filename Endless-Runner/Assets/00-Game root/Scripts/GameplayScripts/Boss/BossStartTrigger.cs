using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<BossManager>().BossActivation();
        Destroy(gameObject);
    }
}
