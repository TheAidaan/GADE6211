using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_Obstacle : Obstacle
{
    private void Start()
    {
    }
    public override void Effect(bool simpleResistance)
    {
        Player.GetComponent<CharacterReact>().Hole();
        base.Effect(simpleResistance);
        
    }

}
