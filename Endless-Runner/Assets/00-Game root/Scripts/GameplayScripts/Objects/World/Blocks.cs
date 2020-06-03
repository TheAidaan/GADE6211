using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : World {

    bool wall = false;
    static int materialIndex = 3;

    //static int counter=0;

    Material[] materials = new Material[12];
    Renderer rend;

    private void Start()
    {
        if (gameObject.layer == 9)
        {
            wall = true;
        }
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        materials = Resources.LoadAll<Material>("Materials/Blocks");

        GetMaterial();

        rend.sharedMaterial = materials[materialIndex];
    }

    void GetMaterial()
    {
        switch (GameManager.CurrentLevel)
        {
            case 1:
                materialIndex = 0;
                break;
            case 2 : materialIndex = 2;
                if(wall)
                {
                    materialIndex--;
                }
                break;
            case 3: materialIndex = 3;
                break;
            default:
                materialIndex = 9;
                break;
        }
    }



    //void ColourCycle()
    //{
        
    //    if (counter < 5)
    //    {
    //        materialIndex++;

    //        if (materialIndex > 9) 
    //        {
    //            materialIndex = 5;
    //        }

    //        counter++;

    //    }
    //    else
    //    {
    //        counter = 0;
    //        materialIndex = 4;
    //    }
        
    //}
}//Blocks
