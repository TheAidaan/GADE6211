using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : Trigger
{
    
    public override void Effect()
    {
        Player.transform.parent.eulerAngles = (transform.eulerAngles * 2);

        base.Effect();
    }
}
