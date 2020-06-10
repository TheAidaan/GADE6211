using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Obstacle : Obstacle
{

    public override void Effect(bool simpleResistance)
    {
        if (simpleResistance)
        {
            Player.GetComponentInChildren<CharacterReact>().Hit();
            Destroy(gameObject);
        }
        else
        {
            base.Effect(simpleResistance);

        }        
    }
}

