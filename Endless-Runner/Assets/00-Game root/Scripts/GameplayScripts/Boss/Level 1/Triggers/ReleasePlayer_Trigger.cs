using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasePlayer_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.transform.parent.CompareTag("Player"))
        {
            if (Boss_1_Tower.RealesePlayer)
            {
                GetComponentInParent<Boss_1_Manager>().ReleasePlayer();
            }
        }
    }
}
