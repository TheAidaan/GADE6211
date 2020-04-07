using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PickUps : SharedBehaviour
{
    GameObject Player;
    Vector3 _rotation;

    public virtual void IdleEffect()
    { 
        transform.Rotate(_rotation); 
    }
    private void OnTriggerEnter(Collider other)
    {
        Player = other.gameObject;
        CollisionEffect(Player);
    }
    public virtual void CollisionEffect(GameObject player)
    {
        Destroy(gameObject);
    }

    public void setRotation(Vector3 Rotation)
    {
        _rotation = Rotation;
    }
}
public abstract class Obstacle : SharedBehaviour
{

    GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        Player = other.gameObject;
        CollisionEffect(Player);

    }

    public virtual void CollisionEffect(GameObject player)
    {
        bool isDead;
        GameObject destroy;

        if (CheckRangeResistant() == false)
        {
            destroy = gameObject;
            isDead = false;

            
        }
        else
        {
            if (Resistant() == false)
            {
                destroy = gameObject;
                isDead = false;
                player.GetComponent<CharacterReact>().setResistance(0, 0);

            }
            else
            {
                destroy = player;
                isDead = true;

            }
            

        }

        GameManager.characterDeath = isDead;
        Destroy(destroy);

    }
    bool CheckRangeResistant()
    {
        bool rangeResistant;

        rangeResistant = Player.GetComponent<CharacterReact>().CheckResistance(true);

        if (rangeResistant)
        {
            return false;
        }
        else 
        { 
            return true;
        }
    }
    bool Resistant()
    {
        bool Resistant;

        Resistant = Player.GetComponent<CharacterReact>().CheckResistance(false);

        if (Resistant)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
public abstract class SharedBehaviour : MonoBehaviour
{
    void Update()
    {
        if (GameManager.characterDeath == false)
        {
            if (gameObject.transform.position.z < GameManager.Player.position.z - 2f)
            {
                Destroy(gameObject);
            }
        }
    }

}




