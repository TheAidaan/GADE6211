using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Obsatcle_1 : Obstacle
{
    GameObject[] Parts = new GameObject[2];
    void Start()
    {
        if (gameObject.GetComponent<Boss_1_OjectController>())
        {
            Destroy(gameObject.AddComponent<Boss_1_OjectController>());
        }

        Parts[0] = transform.GetChild(0).gameObject;
        Parts[1] = transform.GetChild(1).gameObject;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);   
    }
    public override void Effect(bool simpleResistance)
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        gameObject.AddComponent<Boss_1_OjectController>();


    }
}
