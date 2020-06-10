using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsFinished : Trigger
{
    public override void Effect()
    {
        Player.GetComponentInParent<CharacterMovement>().StopForwardMovement(true);
        //Player.transform.position = Vector3.Lerp(Player.transform.position, transform.position, 2f);
        Debug.Log("inplace");

        base.Effect();
    }
}
