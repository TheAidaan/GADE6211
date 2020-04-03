using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEffects : MonoBehaviour
{
    [SerializeField] bool _immunity, _coin, _staticObstacle;

    
    // Update is called once per frame
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

        if (_staticObstacle)
        {
            StaticObstacle obstacle = new StaticObstacle();
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
    



}

 
