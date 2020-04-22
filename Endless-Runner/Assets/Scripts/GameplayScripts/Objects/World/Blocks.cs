using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : World {
    
    [SerializeField] bool wall;

    [SerializeField] bool Starter;

    int materialIndex;

    int currentLevel;

    Material[] materials = new Material[6];
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        materials = Resources.LoadAll<Material>("Materials");

        SetLevel();

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
        switch (currentLevel)
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


    public void SetLevel()
    {
        currentLevel = FindObjectOfType<GameManager>().CurrentLevel(); ;
    }
}//Blocks
