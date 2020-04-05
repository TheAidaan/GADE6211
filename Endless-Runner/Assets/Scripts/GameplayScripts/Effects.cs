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
public class SuperSize : PickUps
{

    public SuperSize(GameObject self)
    {

        self.transform.Rotate(0f, 0f, 3f);

    }
}

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

#endregion

#region character colliders
public class ImmunityCollided : PickUps
{
    public ImmunityCollided(GameObject collided)
    {
        base.CollisionEffect(collided);
    }
}

public class SuperSizeCollided : PickUps
{
    GameObject Self;

    public SuperSizeCollided(GameObject collided)
    {

        base.CollisionEffect(collided);
    }

}
public class FlingCollided : PickUps
{
    GameObject Self;
    bool done;

    public FlingCollided(GameObject collided, GameObject self)
    {
        Self = self;

        Self.transform.position = Vector3.Lerp(Self.transform.position, GoTo(), 3f);
        base.CollisionEffect(collided);
    }
        

    

    Vector3 GoTo()
    {
        return new Vector3(Self.transform.position.x, Self.transform.position.y + 6, Self.transform.position.z);               
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
        base.CollisionEffect(Self, IsDead);
    }

}

public class holeCollided : Obstacle
{
    GameObject Self;
    public holeCollided(GameObject self)
    {
        Self = self;

        Self.transform.position = Vector3.Lerp(Self.transform.position, goTo(), .5f);
    }

    Vector3 goTo()
    {
        return new Vector3(Self.transform.position.x, Self.transform.position.y - 10, Self.transform.position.z);
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




