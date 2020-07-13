using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Boss_3_Manager>().MayAnimate();
    }
}
