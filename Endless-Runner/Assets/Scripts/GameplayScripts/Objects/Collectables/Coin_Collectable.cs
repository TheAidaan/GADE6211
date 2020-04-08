using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collectable : PickUps
{
    Vector3 _rotation = new Vector3(0f, 0f, 3f);


    // Start is called before the first frame update
    void Start()
    {
        setRotation(_rotation);
    }

    // Update is called once per frame
    void Update()
    {
        IdleEffect();
    }

    public override void CollisionEffect(GameObject player)
    {
        player.GetComponent<CharacterStats>().IncreaseCoins(1);
        base.CollisionEffect(gameObject);
    }
}
