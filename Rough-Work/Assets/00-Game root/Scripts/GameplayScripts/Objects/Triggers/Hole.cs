using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Trigger
{
    public override void BounceEffect()
    {
        Player.GetComponentInChildren<CharacterReact>().Bounce(Vector3.up*7500f);
    }

    public override void Effect()
    {
        Player.GetComponent<CharacterReact>().Hole();

    }

}
