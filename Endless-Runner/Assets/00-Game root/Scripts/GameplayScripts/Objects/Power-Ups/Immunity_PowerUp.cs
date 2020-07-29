using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity_PowerUp : PowerUps
{

    void FixedUpdate()
    {
        transform.Rotate(0f, 3f, 0f);
    }

    public override void Effect()
    {
        if (Player.GetComponent<CharacterReact>() != null)
        {
            Player.GetComponent<CharacterReact>().Immunity();
        }
       
        base.Effect();

    }

}
