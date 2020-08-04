using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Obsatcle_1 : Obstacle
{
    readonly GameObject[] Parts = new GameObject[2];
    void Start()
    {
        if (gameObject.GetComponent<Boss_1_ObjectController>())
        {
            Destroy(gameObject.AddComponent<Boss_1_ObjectController>());
        }

        Parts[0] = transform.GetChild(0).gameObject;
        Parts[1] = transform.GetChild(1).gameObject;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);   
    }
    public override void CollisionEffect()
    {
        switch (PlayerResistance())
        {
            case 1:
                Player.GetComponentInChildren<CharacterReact>().Hit();
                Break();
                break;

            case 2: //action done if player has time-based resistance
                Break();
                break;

            default: //action done if player has no resistance
                Break();
                base.Effect();
                break;
        }
    }

    void Break()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        gameObject.AddComponent<Boss_1_ObjectController>();

    }
}
