using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] paths = new Transform[3];

    [SerializeField] Transform enemyObjects;
    [SerializeField] Transform collectorsObjects;
    [SerializeField] Transform powerUpObjects;

    public float playerPosZ;
    float zSpawnPos;
    float ySpawnPos = 0;

    int randNumber;
    int randPos;
    public void Awake()
    {
        Instantiate(paths[0], new Vector3(0, ySpawnPos, 0), paths[0].rotation);
        Instantiate(paths[0], new Vector3(0, ySpawnPos, 0), paths[0].rotation);
    }


    public void SpawnBuildingBlocks()
    {
        if (zSpawnPos <= (playerPosZ + 32f))
        {
            randNumber = Random.Range(0, 3);

           if (randNumber == 1)
            {
                Instantiate(paths[randNumber], new Vector3(0, ySpawnPos, zSpawnPos + 2), paths[randNumber].rotation);
                ySpawnPos += 2;
                zSpawnPos += 10.47f; 

            }
            else
            {
                Instantiate(paths[randNumber], new Vector3(0, ySpawnPos, zSpawnPos), paths[randNumber].rotation);
                zSpawnPos += 4f;
            }

            
            SpawnObjects();
        }
    }

   void SpawnObjects()
    {
        randNumber = Random.Range(0, 10);
        randPos= Random.Range(-1, 2);
        if ((randNumber < 5) && (randNumber > 2))
        {
            Instantiate(collectorsObjects, new Vector3(-1f, ySpawnPos+ 1f, zSpawnPos), collectorsObjects.rotation);
        }
        if (randNumber < 2)
        {
            Instantiate(powerUpObjects, new Vector3(randPos, ySpawnPos+ 2f, zSpawnPos), powerUpObjects.rotation);
        }
        if (randNumber > 6)
        {
            Instantiate(enemyObjects, new Vector3(randPos, ySpawnPos+ 1f, zSpawnPos), enemyObjects.rotation); ;
        }
       
    }
}
