using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    Transform[,] Objects = new Transform[3, 3];
    Transform[] World = new Transform[2];

    [SerializeField] float worldHeight;

    int currentLevel;
    public void AssignObjects()
    {
        World = Resources.LoadAll<Transform>("World");

        switch(currentLevel)
        {
            case 2:Level2();
                break;
            case 3:
                break;
            default: Level1();
                break;
        }
        
    }
   public Transform spawnObject()
    {
        int randNumber;
        int randObject;

        randNumber = Random.Range(0, 10);
        randObject = Random.Range(0, 3);

        if (randNumber < 4)
        {
            return Objects[0, randObject]; //obstacles
            
        }
        else
        {
            if ((3 < randNumber) && (randNumber < 6))
            {
                return Objects[1, 0]; //collector objects
            }
            else
            {
                if ((5 < randNumber) && (randNumber < 8))
                {
                    return Objects[2, randObject]; //Power-Ups
                }
                else
                {
                    return null;
                }
            }

        }
    }

    public void SpawnBuildingBlocks(int zSpawnPoint, Transform Object)
    {
        int randNumber;
        bool maySpawnObject;

        randNumber = Random.Range(-1, 3);
        maySpawnObject = true;



        if (currentLevel > 1)
        {
            randNumber = Random.Range(0, 10);
            if (randNumber > 9)
            {
                worldHeight++;
            }

        }

        if (Object == null)
        {
            randNumber = 4;
        }

        Instantiate(World[1], new Vector3(-2f, worldHeight, zSpawnPoint), World[1].rotation);
        for (int i = -1; i < 2; i++)
        {
            
            if ((i == randNumber) && (maySpawnObject == true))
            {
                if (Object.gameObject.name == "2.Hole")
                {
                    
                    Instantiate(Object, new Vector3(i, worldHeight, zSpawnPoint), Object.rotation);
                    maySpawnObject = false;
                }else
                {
                    Instantiate(World[0], new Vector3(i, worldHeight, zSpawnPoint), World[0].rotation);
                    Instantiate(Object, new Vector3(i, worldHeight+1, zSpawnPoint), Object.rotation);
                    maySpawnObject = false;
                }               
            }else
            {
              Instantiate(World[0], new Vector3(i, worldHeight, zSpawnPoint), World[0].rotation);
            }
        }
        Instantiate(World[1], new Vector3(2f, worldHeight, zSpawnPoint), World[1].rotation);
    }

    public void SetLevel(int Level)
    {
        currentLevel = Level;
    }
    void Level1()
    {
        Transform[] enemyObjects = new Transform[3];
        Transform[] collectorsObjects = new Transform[3];
        Transform[] powerUpObjects = new Transform[3];

        enemyObjects = Resources.LoadAll<Transform>("Level1/enemyObjects");
        collectorsObjects = Resources.LoadAll<Transform>("Level1/collectorsObjects");
        powerUpObjects = Resources.LoadAll<Transform>("Level1/powerUpObjects");


        for (int i = 0; i < enemyObjects.Length; i++)
        {
            Objects[0, i] = enemyObjects[i];

        }

        for (int i = 0; i < collectorsObjects.Length; i++)
        {
            Objects[1, i] = collectorsObjects[i];

        }

        for (int i = 0; i < powerUpObjects.Length; i++)
        {
            Objects[2, i] = powerUpObjects[i];

        }
    }

    void Level2()
    {
        Level1();
        Objects[0, 0] = null;
    }

}

