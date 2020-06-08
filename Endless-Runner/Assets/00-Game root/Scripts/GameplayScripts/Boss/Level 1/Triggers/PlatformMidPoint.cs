using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMidPoint : Trigger
{

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Boss_1_Spawner>().ActivateWalkway();

        if (Boss_1_Manager.CurrrentStage == 4)
        {
            GetComponentInParent<Boss_1_Manager>().ReleasePlayer();
        }


    }
}
