using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCenter : Trigger
{
    public override void Effect()
    {
        Player.transform.LookAt(transform.parent);
        Debug.Log("looking");
        base.Effect();
    }
}
