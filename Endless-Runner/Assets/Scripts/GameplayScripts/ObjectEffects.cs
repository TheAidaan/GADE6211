using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEffects : MonoBehaviour  
{
    
    
    bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        isPlayer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "PowerUp(Clone)")
        {
            transform.Rotate(3, 0, 0);
        }

        if (gameObject.name == "Coin(Clone)")
        {
            transform.Rotate(0, 0, 3);
        }


    }
}
