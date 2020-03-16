using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    public virtual void IdleEffect(float xROt, float yRot, float zRot)
    {
        transform.Rotate(xROt, yRot, zRot);
    }
    public virtual void OnCollisionEnter(Collision other)
    {
        CollisionEffect();
    }

    public virtual void CollisionEffect()
    {
        Destroy(gameObject);
    }

}


public abstract class Obstacle : MonoBehaviour
{
    public virtual void CollisionEffect()
    {
        GameManager.characterDeath = true; Destroy(gameObject);
    }
    public virtual void OnCollisionEnter(Collision other)
    {
        CollisionEffect();
    }

}


public abstract class SharedBehaviour : MonoBehaviour
{
    public virtual void SelfDestruct()
    {
        if (GameManager.characterDeath == false)
        {
            if (transform.position.z < GameManager.Player.position.z - 10f)
            {
                Destroy(gameObject);
            }
        }
    }

}


#region PickUp Subclasses


public class Coin : PickUps
{

    public Coin()
    {
        IdleEffect(0f, 0f, 3f);

        CollisionEffect();
    }

    public override void CollisionEffect()
    {
        GameManager.coinTotal++;

        base.CollisionEffect();
    }

}

public class Immunity : PickUps
{

    public Immunity()
    {
        IdleEffect(3f, 0f, 0f);

        CollisionEffect();
    }

    public override void CollisionEffect()
    {
        CharacterEffects.resistant = true;
        CharacterEffects.Object.material = CharacterEffects.blue;

        base.CollisionEffect();
    }

}


#endregion

#region Obstacle Subclasses
public class StaticObstacle : Obstacle
{

    public StaticObstacle()
    {

        CollisionEffect();
    }



}
#endregion




