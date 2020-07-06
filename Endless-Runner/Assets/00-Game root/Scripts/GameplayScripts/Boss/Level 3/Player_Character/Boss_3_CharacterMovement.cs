using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_CharacterMovement : MonoBehaviour
{
    GameObject target;
  
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.RotateAround(target.transform.position, Vector3.down, .2f);
    }

}
