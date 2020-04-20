using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : World {
    
    [SerializeField] bool wall;

    [SerializeField] bool Starter;

    int materialIndex;

    int level;

    Material[] materials = new Material[6];
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        materials = Resources.LoadAll<Material>("Materials");
        if (Starter)
        {
            materialIndex = 4;
        }else
        {
            getMaterial();
        }
        

        rend.sharedMaterial = materials[materialIndex];
    }

    void getMaterial()
    {
        level = FindObjectOfType<GameManager>().CurrentLevel();
        switch (level)
        {
            case 2 : materialIndex = 7;
                if(wall)
                {
                    materialIndex--;
                }
                break;
            case 3: materialIndex = 8;
                break;
            default:materialIndex = 5;
                break;
        }
    }

}//Blocks
