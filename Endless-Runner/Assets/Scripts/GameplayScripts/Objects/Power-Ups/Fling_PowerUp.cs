using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling_PowerUp : PickUps
{
    bool PlayerResistant;
    public override void CollisionEffect(GameObject player)
    {
        PlayerResistant = player.GetComponent<CharacterReact>().CheckResistance(true);
        if (PlayerResistant == false)
        {
            player.GetComponent<CharacterReact>().Fling();
        }
        base.CollisionEffect(player);
    }
}
