using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_ObjectDestoryer : MonoBehaviour
{
    float initalAngle;
    void Start()
    {
        initalAngle = transform.parent.eulerAngles.y;
        if (initalAngle == 0)
        {
            initalAngle = 360;
        }
    }
    void FixedUpdate()
    {
        if (BossManager.bossActive)
        {
            float currentAngle = transform.parent.eulerAngles.y;
            float AngleMagnitude = Mathf.Abs(currentAngle - initalAngle);

            if (AngleMagnitude > 180)
            {
                Destroy(gameObject);
            }
        }else
        { 
            Destroy(GetComponent<Boss_1_ObjectDestoryer>()); 
        }
    }
}
