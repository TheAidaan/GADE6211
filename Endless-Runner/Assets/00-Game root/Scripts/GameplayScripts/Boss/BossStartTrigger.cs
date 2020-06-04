using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartTrigger : Trigger
{
    public override void Effect()
    {
        FindObjectOfType<BossManager>().BossActivation();

        base.Effect();
    }
}
