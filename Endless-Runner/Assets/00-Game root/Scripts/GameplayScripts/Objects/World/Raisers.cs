using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raisers : Collectable
{
    Rigidbody playerRb;

    public override void Effect()
    {
        playerRb = Player.GetComponentInParent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 350,ForceMode.Impulse);

        base.Effect();
    }
}
