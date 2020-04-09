using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    Transform[,] Objects = new Transform[3, 3];
    Transform[] World = new Transform[2];
    public void AssignObjects()
    {

        Transform[] enemyObjects = new Transform[3];
        Transform[] collectorsObjects = new Transform[3];
        Transform[] powerUpObjects = new Transform[3];

        enemyObjects = Resources.LoadAll<Transform>("Level1/enemyObjects");
        collectorsObjects = Resources.LoadAll<Transform>("Level1/collectorsObjects");
        powerUpObjects = Resources.LoadAll<Transform>("Level1/powerUpObjects");

        World = Resources.LoadAll<Transform>("World");


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

        if (Object == null)
        {
            randNumber = 4;
        }

        Instantiate(World[1], new Vector3(-2f, .2f, zSpawnPoint), World[1].rotation);
        for (int i = -1; i < 2; i++)
        {
            
            if ((i == randNumber) && (maySpawnObject == true))
            {
                if (Object.gameObject.name == "2.Hole")
                {
                    
                    Instantiate(Object, new Vector3(i, 0f, zSpawnPoint), Object.rotation);
                    maySpawnObject = false;
                }else
                {
                    Instantiate(World[0], new Vector3(i, 0f, zSpawnPoint), World[0].rotation);
                    Instantiate(Object, new Vector3(i, 1f, zSpawnPoint), Object.rotation);
                    maySpawnObject = false;
                }               
            }else
            {
              Instantiate(World[0], new Vector3(i, 0f, zSpawnPoint), World[0].rotation);
            }
        }
        Instantiate(World[1], new Vector3(2f, .2f, zSpawnPoint), World[1].rotation);
        


    }

}

