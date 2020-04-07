using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_Obstacle : Obstacle
{
    GameObject Player;
    bool PlayerResistant;
    public override void CollisionEffect(GameObject player)
    {
        Player = player;
        PlayerResistant = Player.GetComponent<CharacterReact>().CheckResistance(true);

        if (PlayerResistant == false)
        {
            player.GetComponent<CharacterMovement>().StopForward(true);
            GameManager.characterDeath = true;
            Player.transform.position = Vector3.Lerp(Player.transform.position, GoTo(), .5f);
        }
        
    }

    Vector3 GoTo()
    {
        return new Vector3(Player.transform.position.x, Player.transform.position.y - 10, Player.transform.position.z);
    }
}
