using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Trigger
{
    public override void BounceEffect()
    {
        if (Player.GetComponent<CharacterReact>() != null)
        {
            Player.GetComponentInChildren<CharacterReact>().Bounce(Vector3.up * 7500f);
        }
        
    }

    public override void Effect()
    {
        if(Player.GetComponent<CharacterReact>()!= null)
        {
            Player.GetComponent<CharacterReact>().Hole();
        }
        

    }

}
