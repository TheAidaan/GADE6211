using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_PowerUps : Collectable
{
    public override void Effect()
    {
        Player.GetComponent<CharacterStats>().ChangePower(25f) ;
        base.Effect();
    }
}
