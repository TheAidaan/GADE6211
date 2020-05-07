using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gone : Collectable
{
    public override void Effect()
    {
        Player.GetComponent<CharacterReact>().Gone();

    }
}
