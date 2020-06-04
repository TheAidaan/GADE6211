using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Cylinder : MonoBehaviour
{
    
    void Update()
    {
        if (BossManager.bossActive)
        {
            transform.Translate(0, -0.05f, 0);
        }
    }
}
