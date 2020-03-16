using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEffects : MonoBehaviour
{
    [SerializeField] bool _immunity, _coin,_staicObstacle;


    // Update is called once per frame
    void Update()
    {

        if (_immunity)
        {
            Immunity immunity = new Immunity();
        }

        if (_coin)
        {
            Coin coin = new Coin();
        }

        if (_staicObstacle)
        {
            StaticObstacle obstacle = new StaticObstacle();
        }
        
        
        SelfDestruct selfDestruct = new SelfDestruct();



    }
}

 
