using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : World
{
    int materialIndex;

    Material[] materials = new Material[3];
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        materials = Resources.LoadAll<Material>("Materials/Blocks");


        rend.sharedMaterial = materials[GameManager.CurrentLevel-1];
    }

}
