using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFling_PowerUp : PowerUps
{
    public override void Effect() 
    {
        Player.GetComponent<CharacterReact>().Fling(true);

        FindObjectOfType<GameManager>().ChangeLevel();

        base.Effect();
    }
}
