using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling_PowerUp : PowerUps
{
    public override void Effect() 
    {
        if (Player.GetComponent<CharacterReact>() != null)
        {
            Player.GetComponent<CharacterReact>().Fling();
        }
        base.Effect();
    }
}
