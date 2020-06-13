using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsFinished : Trigger
{
    public override void Effect()
    {
        Player.GetComponentInParent<CharacterMovement>().StopForwardMovement(true);
        Player.GetComponentInParent<CharacterMovement>().LockControls(true);
        GetComponentInParent<Boss_1_InsideTower>().PlayerIsInPlace(true);

        base.Effect();
    }
}
