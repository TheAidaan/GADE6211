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

    

    public override void DestroyCondition()
    {
        HeightDestroy();
    }

}

public abstract class Obstacle : Objects
{
    static int _obstaclesPassed = 0;
    bool _counted = false;

    private void updateObstaclesPassed()
    {
        _obstaclesPassed++;
        print("Obstacles Passed " + _obstaclesPassed);
        GameManager.gameManager.updateMetrics -= updateObstaclesPassed;
    }

    public override void Update()
    {
        base.Update();
        if (!GameManager.characterDeath)
        {
            if ((transform.position.z < GameManager.Player.position.z) && (!_counted))
            {
                GameManager.gameManager.updateMetrics += updateObstaclesPassed;
                _counted = true;
            }
        }
    }

    public virtual void Effect() 
    { 
        if (Player.GetComponent<CharacterReact>()!= null) 
        { 
            Player.GetComponent<CharacterReact>().Die(true, true);
        }else
        {
            Destroy(gameObject);
        }
    }
    public override void CollisionEffect()
    { 
        switch (PlayerResistance())
        {
            case 1: Player.GetComponentInChildren<CharacterReact>().Hit();
                Destroy(gameObject);
                break;

            case 2: //action done if player has time-based resistance
                Destroy(gameObject);
                break;

            default: //action done if player has no resistance
                Effect();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public override void DestroyCondition()
    {
        HeightDestroy();
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
    float _distance;
    public virtual void Update()
    {

        DestroyCondition();
    }
    public virtual void DestroyCondition() { DistanceDestroy(); }
    public virtual void DistanceDestroy()
    {
        if (!GameManager.characterDeath)
        {
            if (gameObject.transform.position.z < GameManager.Player.position.z - 6)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void HeightDestroy()
    {
        bool onGround = Physics.Raycast(transform.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        
        if (!onGround)
        { 
            Destroy(gameObject);
        }
    }
    
}

public abstract class Objects : World
{

    public GameObject Player;

    void OnTriggerEnter(Collider collision) // All objects except obstacles,excluding hole
    { 

        Player = collision.gameObject;

        CollisionEffect();
    }

    public virtual int PlayerResistance()
    {
        if (Player.GetComponent<CharacterReact>() !=null)
        {
            return Player.GetComponent<CharacterReact>().PlayerResistance();
        }else
        { 
            return 0; 
        }
        
    }
    public virtual void CollisionEffect() { }


}




