using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_CharacterMovement : Movement_Abstract
{
   
    CharacterMovement movement;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
       
    }

    public override void TurnAround()
    {
        base.TurnAround();


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


