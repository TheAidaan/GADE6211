using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity_PowerUp : PowerUps
{

    void Start()
    {
        Rotation = new Vector3(3f, 0f, 0f);
            
    }

    void Update()
    {
        IdleEffect();
    }

    public override void Effect()
    {
        Player.GetComponent<CharacterReact>().Immunity();
        base.Effect();

    }

}
