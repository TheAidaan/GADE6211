using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
   
    void Update()
    {
        if (GameManager.characterDeath == false)
        {
            if (transform.position.z < GameManager.Player.position.z-10f)
            {
                Destroy(gameObject);
            }
        }

    }
}