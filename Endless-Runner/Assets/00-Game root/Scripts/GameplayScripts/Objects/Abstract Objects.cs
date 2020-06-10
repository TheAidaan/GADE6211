using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PowerUps : Objects
{
    public virtual void Effect() { Destroy(gameObject); }
    public override void CollisionEffect()
    {
        switch (PlayerResistance())
        {
            case 1: //action done if player has simple resistance
                Effect();
                break;

            case 2: //action done if player has time-based resistance
                Destroy(gameObject);
                break;

            default: //action done if player has no resistance
                Effect();
                break;
        }
    }
    public override void IdleEffect()
    {
        transform.Rotate(Rotation);
    }

}

public abstract class Collectable : Objects
{
    public virtual void Effect() { Destroy(gameObject); }
    public override void CollisionEffect()
    {
        Effect();
    }
    public override void IdleEffect()
    {
        transform.Rotate(Rotation);
    }
}

public abstract class Obstacle : Objects
{
    public virtual void Effect(bool simpleResistance) { Player.GetComponent<CharacterReact>().Die(true,true);}
    public override void CollisionEffect()
    { 
        
        switch (PlayerResistance())
        {
            case 1: //action done if player has simple resistance
                Effect(true);
                break;

            case 2: //action done if player has time-based resistance
                Destroy(gameObject);
                break;

            default: //action done if player has no resistance
                Effect(false);
                break;
        }
    }
}

public abstract class Trigger : Objects
{
    public virtual void Effect() { Destroy(gameObject); }
    public virtual void BounceEffect() { }
    public override void CollisionEffect()
    {
        switch (PlayerResistance())
        {
            case 1: //action done if player has simple resistance
                Effect();
                break;

            case 2: //action done if player has time-based resistance
                BounceEffect();
                break;

            default: //action done if player has no resistance
                Effect();
                break;
        }
    }

}

public abstract class World : MonoBehaviour
{
    void Update()
    {
        if (GameManager.characterDeath == false)
        {
            if (gameObject.transform.position.z < GameManager.Player.position.z - 6f)
            {
                Destroy(gameObject);
            }
        }

    }
}

public abstract class Objects : World
{
    void Update()
    {

        IdleEffect();
    }
    public Vector3 Rotation;
    public GameObject Player;

    private void OnTriggerEnter(Collider collision) // All objects except obstacles,excluding hole
    { 

        Player = collision.gameObject;

        CollisionEffect();
    }

    public virtual int PlayerResistance()
    {
        return Player.GetComponent<CharacterReact>().PlayerResistance();
    }

    public virtual void CollisionEffect() { }

    public virtual void IdleEffect() { }


}




