using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Hammer : Obstacle
{
    public override void Effect()
    {
        if (Player.GetComponent<CharacterReact>() != null)
        {
            Player.GetComponent<CharacterReact>().Die(true, true);
        }

    }
    public override void CollisionEffect()
    {
        switch (PlayerResistance())
        {
            case 1:
                Player.GetComponentInChildren<CharacterReact>().Hit();
                break;

            case 2: //action done if player has time-based resistance
                break;

            default: //action done if player has no resistance
                Effect();
                break;
        }
    }

    public override void DestroyCondition() { }
}
