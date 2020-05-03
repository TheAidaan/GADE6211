using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoving_Obstacle : Obstacle
{
    Vector3 GoTo;
   
    void Start()
    {
       
            if (Random.Range(0, 2) == 0)
            {
                GoTo = new Vector3(2f, transform.position.y, transform.position.z);
            }
            else
            {
                GoTo = new Vector3(-2f, transform.position.y, transform.position.z);
            }
        
    }

    void Update()
    {
        IdleEffect();
    }

    public override void IdleEffect()
    {
        transform.position = Vector3.Lerp(transform.position, GoTo, .01f);
        if ((transform.position.x > 1) || (transform.position.x < -1))
        {
            ChangeSlide();
        }

    }
       
    void ChangeSlide()
    {
        if (GoTo.x < 0)
        {
            GoTo = new Vector3(2f, transform.position.y, transform.position.z);
        }
        else
        {
           GoTo = new Vector3(-2f, transform.position.y, transform.position.z);
        }
    }

    public override void Effect(bool simpleResistance)
    {
        if (simpleResistance)
        {
            Player.GetComponentInChildren<CharacterReact>().Hit();
            Destroy(gameObject);
        }
        else
        {
            base.Effect(simpleResistance);
        }
    }
}
