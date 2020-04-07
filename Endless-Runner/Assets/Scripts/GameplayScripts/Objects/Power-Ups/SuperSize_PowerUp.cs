using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSize_PowerUp : PickUps
{
    GameObject Player;
    Vector3 _rotation = new Vector3(0f, 0f, 3f);
    bool PlayerResistant;

    void Start()
    {
        setRotation(_rotation);
    }

    void Update()
    {
        IdleEffect();
    }

    public override void CollisionEffect(GameObject player)
    {
        Player = player;
        PlayerResistant = player.GetComponent<CharacterReact>().CheckResistance(true);
        if (PlayerResistant == false)
        {
            player.GetComponent<CharacterReact>().setResistance(2, 2);
            player.GetComponent<CharacterReact>().SuperSize();
        }
        base.CollisionEffect(player);
    }

    IEnumerator resistantPeriod()
    {
        Player.transform.localScale = new Vector3(3f, 3f, 3f);
        Player.GetComponent<CharacterMovement>().superSized(true, 2);

        yield return new WaitForSeconds(0f);

        Player.GetComponent<CharacterMovement>().superSized(false, .9f);
        Player.transform.localScale = new Vector3(.8f, .8f, .8f);

    }
}
