using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gone : Trigger
{
    public override void BounceEffect()
    {
        Player.GetComponentInChildren<CharacterReact>().Bounce(Vector3.back* 10000f);
    }

    public override void Effect()
    {
        Player.GetComponent<CharacterReact>().Gone();

    }
}
