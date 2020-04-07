using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity_PowerUp : PickUps
{
    bool PlayerResistant;
    Vector3 _rotation = new Vector3(3f,0f,0f);

    void Start()
    {
        setRotation(_rotation);
            
    }

    void Update()
    {
        IdleEffect();
    }
    public override void CollisionEffect(GameObject player)
    {
        PlayerResistant = player.GetComponent<CharacterReact>().CheckResistance(true);
        if (PlayerResistant == false )
        {
            player.GetComponent<CharacterReact>().setResistance(1, 1);
        }
        base.CollisionEffect(gameObject);
    }


}
