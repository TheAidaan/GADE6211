using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collectable : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        Rotation = new Vector3(0f, 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        IdleEffect();
    }

    public override void Effect()
    {
        Player.GetComponent<CharacterStats>().IncreaseCoins(1);
        base.Effect();
    }
}
