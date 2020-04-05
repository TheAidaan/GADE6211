using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEffects : MonoBehaviour
{
    [SerializeField] bool _immunity,_superSize, _coin, _staticObstacle,_movingObstacle, _hole;

    Vector3 movingObstacleGoTo;

    private void Start()
    {
        if (_movingObstacle)
        {
            if (Random.Range(0, 2) == 0)
            {
                movingObstacleGoTo = new Vector3(2f, transform.position.y, transform.position.z);
            }
            else
            {
                movingObstacleGoTo = new Vector3(-2f, transform.position.y, transform.position.z);
            }
        }
        
    }

    void Update()
    {

        if (_immunity)
        {
            Immunity immunity = new Immunity(gameObject);
        }

        if (_coin)
        {
            Coin coin = new Coin(gameObject);
        }
        if (_superSize)
        {
            SuperSize superSize = new SuperSize(gameObject);
        }

        if (_staticObstacle)
        {
            StaticObstacle obstacle = new StaticObstacle();
        }

        if (_movingObstacle)
        {
            MovingObstacle movingObstacle = new MovingObstacle(gameObject,movingObstacleGoTo);
            if ((transform.position.x > 1) || (transform.position.x < -1))
            {
                ChangeSlide();
            }
        }

        AllPrefabs selfDestruct = new AllPrefabs(gameObject);
       
    }

    public bool isImmunity()
    {
        return _immunity;
    }
    public bool isCoin()
    {
        return _coin;
    }
    public bool isStaticObstacle()
    {
        return _staticObstacle;
    }
    public bool isMovingObstacle()
    {
        return _movingObstacle;
    }
    public bool isHole()
    {
        return _hole;
    }
    public bool isSuperSize()
    {
        return _superSize;
    }

    void ChangeSlide()
    {
        if (movingObstacleGoTo.x < 0)
        {
            movingObstacleGoTo = new Vector3(2f, transform.position.y, transform.position.z);
        }
        else
        {
            movingObstacleGoTo = new Vector3(-2f, transform.position.y, transform.position.z);
        }
       
    }

}


