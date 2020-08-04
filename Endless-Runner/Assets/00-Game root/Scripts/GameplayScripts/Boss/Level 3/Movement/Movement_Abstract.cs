using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement_Abstract : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        if (Boss_3_Manager.bossActive)
        {
            if (Boss_3_Manager.ClockwiseRotation)
            {
                transform.RotateAround(target.transform.position, Vector3.up, .2f);
            }
            else
            {
                transform.RotateAround(target.transform.position, Vector3.down, .29f);
            }
        }
    }

    public virtual void TurnAround()
    {
        transform.Rotate(Vector3.up * 180);
    }
}
