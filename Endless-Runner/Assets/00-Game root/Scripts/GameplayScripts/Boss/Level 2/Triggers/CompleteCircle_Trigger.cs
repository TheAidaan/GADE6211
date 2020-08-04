using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCircle_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Boss_1_PathController>().CompleteCircle();
        Destroy(gameObject);

    }
}
