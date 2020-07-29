using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_CharacterMovement : MonoBehaviour
{
    GameObject target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (BossManager.bossActive)
        {
            transform.RotateAround(target.transform.position, Vector3.up, .29f);
        }
        
    }
}
