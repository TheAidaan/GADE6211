using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gone : Trigger
{
    public override void BounceEffect()
    {
        if (Player.GetComponentInChildren<CharacterReact>() != null)
        {
            Player.GetComponentInChildren<CharacterReact>().Bounce(Vector3.back * 10000f);
        }

    }

    public override void Effect()
    {
        if (Player.GetComponent<CharacterReact>()!= null)
        {
            Player.GetComponent<CharacterReact>().Gone();
        }
    }
}
