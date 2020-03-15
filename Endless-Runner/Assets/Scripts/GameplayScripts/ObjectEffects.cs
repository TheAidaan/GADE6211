using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Obstacle :MonoBehaviour{
   
}
abstract class PickUps : MonoBehaviour{
    public abstract void IdleEffect();
    public abstract void CollisionEffect();
}
abstract class CollectorObjects : MonoBehaviour {}

class Immunity : PickUps 
{
    GameObject Self;
    public Immunity(GameObject self)
    {
        Self = self;
    }
    
        public override void IdleEffect()
    {
        Self.transform.Rotate(3, 0, 0);
    }
    public override void CollisionEffect()
    {
        CharacterEffects.resistant = true;
        CharacterEffects.Object.material = CharacterEffects.blue;

        Destroy(Self);
    }

    
    
}

public class ObjectEffects : MonoBehaviour
{
    [SerializeField] bool immunity, coin, block;
    private void Update()
    {
        if (immunity)
        {
            Immunity immunity = new Immunity(gameObject);
                       
        }

        if (coin)
        {

        }

        if (block)
        {

        }

    }
}



