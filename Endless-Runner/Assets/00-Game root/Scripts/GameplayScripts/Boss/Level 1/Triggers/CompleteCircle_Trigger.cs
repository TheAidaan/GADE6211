using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCircle_Trigger : Trigger
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Boss_1_PathController>().CompleteCircle();

    }
}
