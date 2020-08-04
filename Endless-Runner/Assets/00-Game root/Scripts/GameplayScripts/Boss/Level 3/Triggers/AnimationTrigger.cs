using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.transform.parent != null))
        {
            if (other.gameObject.transform.parent.CompareTag("Player"))
            {
                GetComponentInParent<Boss_3_Manager>().MayAnimate();
            }

        } 
    }
}
