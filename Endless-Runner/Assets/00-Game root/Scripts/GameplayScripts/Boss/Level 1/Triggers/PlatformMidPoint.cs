using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMidPoint : Trigger
{
    public override void Effect()
    {
        FindObjectOfType<Boss_1_spawner>().SpawnWalkway();
        base.Effect();
    }
}
