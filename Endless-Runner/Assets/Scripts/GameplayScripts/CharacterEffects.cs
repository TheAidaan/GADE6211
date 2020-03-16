using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{

    public static bool resistant;

    public static Material orange;
    public static Material blue;

    public static Renderer Object;

    bool _immunity, _coin, _staticObstacle;
    void Awake()
    {
        protectPlayer();
    }
    void Update()
    {
        if (resistant==true)
        {
            Object.material = blue;
        }
        else
        {
            Object.material = orange;
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        _immunity = other.gameObject.GetComponent<ObjectEffects>().isImmunity();
        _coin = other.gameObject.GetComponent<ObjectEffects>().isCoin();
        _staticObstacle = other.gameObject.GetComponent<ObjectEffects>().isStaticObstacle();


        if (_immunity)
        {
            ImmunityCollided immunityCollided = new ImmunityCollided(other.gameObject);
        }

        if (_coin)
        {
            CoinCollided coinCollided = new CoinCollided(other.gameObject);
        }

        if (_staticObstacle)
        {
            if (resistant==true)
            {
                ObstacleCollided obstacleCollided = new ObstacleCollided(other.gameObject, false);
            }
            else
            {
                ObstacleCollided obstacleCollided = new ObstacleCollided(gameObject,true);
                resistant = false;
            }
            
        }
    }
    IEnumerator protectPlayer()
    {
        resistant = true;
        yield return new WaitForSeconds(10f);
        resistant = false;
    }
}
