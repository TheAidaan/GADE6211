using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_Obstacle : Obstacle
{

    public override void Effect(bool simpleResistance)
    {
        Player.GetComponent<CharacterReact>().Hole();

    }

}
