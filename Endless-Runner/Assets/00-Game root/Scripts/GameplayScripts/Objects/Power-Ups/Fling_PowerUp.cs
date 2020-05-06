using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling_PowerUp : PowerUps
{
    public override void Effect() 
    {
        Player.GetComponent<CharacterReact>().Fling(false);
        base.Effect();
    }
}
