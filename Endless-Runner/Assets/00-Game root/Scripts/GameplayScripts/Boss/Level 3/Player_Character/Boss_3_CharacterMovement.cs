using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_CharacterMovement : MonoBehaviour
{
    GameObject target;
    CharacterMovement movement;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        target = GameObject.FindGameObjectWithTag("Boss");
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (Boss_3_Manager.ClockwiseRotation)
        {
            transform.RotateAround(target.transform.position, Vector3.up, .2f);
        }
        else
        {
            transform.RotateAround(target.transform.position, Vector3.down, .2f);
        }  
    }

    public bool InSafeZone()
    {
        return Physics.Raycast(transform.position, Vector3.down, 5f, LayerMask.GetMask("Ground"));
    }

    public void TurnAround()
    {
        transform.Rotate(Vector3.up * 180);


        if (CharacterMovement.CurrentLane == 1)
        {
            movement.SetLane(3);
        }else
        {
            if (CharacterMovement.CurrentLane == 3)
            {
            movement.SetLane(1);
            }
        }

    }
}


