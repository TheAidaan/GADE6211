using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{

    public static bool resistant;

    bool _immunity, _coin, _staticObstacle;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
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
            if (resistant == true)
            {
                ObstacleCollided obstacleCollided = new ObstacleCollided(other.gameObject, false);
            }
            else
            {
                ObstacleCollided obstacleCollided = new ObstacleCollided(gameObject, true);
                resistant = false;
            }

        }

    }
}
