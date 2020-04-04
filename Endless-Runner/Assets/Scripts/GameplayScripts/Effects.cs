using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    public virtual void IdleEffect(){}

    public virtual void CollisionEffect(GameObject destroy)
    {
        Destroy(destroy);
    }

}


public abstract class Obstacle : MonoBehaviour
{
    
    public virtual void CollisionEffect(GameObject destroy, bool isDead)
    {
        GameManager.characterDeath = isDead; 
        Destroy(destroy);
    }

}


public abstract class SharedBehaviour : MonoBehaviour
{
    public virtual void SelfDestruct(GameObject self)
    {
        if (GameManager.characterDeath == false)
        {
            if (self.transform.position.z < GameManager.Player.position.z - 2f)
            {
                Destroy(self);
            }
        }
    }

}


#region PickUp Subclasses


public class Coin : PickUps
{
    public Coin(GameObject self)
    {
        self.transform.Rotate(0f, 0f, 3f);

    }

}

public class Immunity : PickUps
{

    public Immunity(GameObject self)
    {
        self.transform.Rotate(3f, 0f, 0f);

    }

    

}

#region character colliders
public class ImmunityCollided: PickUps
{
    GameObject Collided;  
    public ImmunityCollided(GameObject collided)
    {
        
        Collided = collided;
        CollisionEffect(Collided);
    }
    public override void CollisionEffect(GameObject destroy)
    {
        CharacterEffects.resistant = true;
        base.CollisionEffect(Collided);
    }
}

public class CoinCollided : PickUps
{
    GameObject Collided;
    public CoinCollided(GameObject collided)

    {
        Collided = collided;
        GameManager.coinTotal++;
        base.CollisionEffect(Collided);
    }
}

public class ObstacleCollided : Obstacle
{
    bool IsDead;
    GameObject Self;
    public ObstacleCollided(GameObject self, bool isDead)
    {
        IsDead = isDead;
        Self = self;
        base.CollisionEffect(Self,IsDead);
    }
        
}
#endregion

#endregion

#region Obstacle Subclasses
public class StaticObstacle : Obstacle
{

    public StaticObstacle()
    {
        //void OnCollisionEnter(Collision other);

    }

}

public class MovingObstacle : Obstacle
{ 
    public MovingObstacle(GameObject self, Vector3 goTo)
    {
        self.transform.position = Vector3.Lerp(self.transform.position, goTo, .05f);   
    }
}

public class hole : Obstacle
{
    public hole(GameObject self)
    {
        self.transform.Translate(Vector3.down * 4 * Time.deltaTime);
        CollisionEffect(self, true);
    }
}
#endregion

public class AllPrefabs : SharedBehaviour
{
    public AllPrefabs(GameObject self) 
    {
        base.SelfDestruct(self);
    }
}




