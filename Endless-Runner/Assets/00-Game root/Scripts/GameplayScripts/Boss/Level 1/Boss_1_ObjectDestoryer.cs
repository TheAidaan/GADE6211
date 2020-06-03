using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_ObjectDestoryer : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.rotation.y < -0.5f)
        {
            Destroy(gameObject);
        }
    }
}
