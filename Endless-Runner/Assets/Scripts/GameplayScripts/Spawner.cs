using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    Transform[,] Objects = new Transform[3, 10];
    Transform[] World = new Transform[2];

    int heightChangePoint=0;

    int LastStumpPoint=0;

    [SerializeField] float worldHeight;

    int currentLevel;
    public void AssignObjects()
    {
        World = Resources.LoadAll<Transform>("World");


        Transform[] enemyObjects = new Transform[6];
        Transform[] collectorsObjects = new Transform[3];
        Transform[] powerUpObjects = new Transform[6];

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


        switch (currentLevel)
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
        randObject = Random.Range(0, 4);

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
                    if (randObject == 3)
                    {
                        randObject = 2;
                    }
                    return Objects[2, randObject];//Power-Ups
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

        if ((currentLevel > 1) && (heightChangePoint<zSpawnPoint-8))
        {
            randNumber = Random.Range(0, 100);

            if (randNumber < 11)
            {
                heightChangePoint = zSpawnPoint;
                worldHeight+= 4;
                spawnRaiser(zSpawnPoint-1);
                Object = null;
            }
        }
        if (heightChangePoint>=zSpawnPoint-2)
        {
            Object = null;
        }

        if (Object == null)
        {
            randNumber = 4;
        }
        else
        {
            if (Object.gameObject.name == "3.Stump")
            {
                if (LastStumpPoint < (zSpawnPoint - 3))
                {
                    randNumber = 0;
                    LastStumpPoint = zSpawnPoint;
                }else
                {
                    randNumber = 4;
                }   
            }         
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

    public void SetLevel()
    {
        currentLevel = GetComponent<GameManager>().CurrentLevel();
    }
    void Level1()
    {
        Objects[0, 3] = null;
    }

    void Level2()
    {
        Objects[0, 0] = null;
    }

    void spawnRaiser(int zSpawnPoint)
    {
        int i;
        i = Random.Range(-1, 2);
        Instantiate(Objects[2, 3], new Vector3(i, worldHeight-3, zSpawnPoint), Objects[2, 3].rotation);

    }

}

