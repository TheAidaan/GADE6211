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

    public int randNumber;

     public void SpawnBuildingBlocks()
    {
        if (zSpawnPos <= (playerPosZ + 32f))
        {
            randNumber = Random.Range(0, 3);
            Instantiate(paths[randNumber], new Vector3(0, ySpawnPos, zSpawnPos), paths[randNumber].rotation);

            if (randNumber == 1)
            {
                
                ySpawnPos += 2;
                zSpawnPos += 7.47f;
            }else
            {
                zSpawnPos += 4f;
            }

            
            SpawnObjects();
        }
    }

   void SpawnObjects()
    {
        randNumber = Random.Range(0, 10);
        if (randNumber < 3)
        {
            Instantiate(collectorsObjects, new Vector3(-1f, ySpawnPos+ 1f, zSpawnPos), collectorsObjects.rotation);
        }
        if (randNumber < 7)
        {
            Instantiate(powerUpObjects, new Vector3(1f, ySpawnPos+ 1f, zSpawnPos), powerUpObjects.rotation);
        }
        if (randNumber == 4)
        {
            Instantiate(enemyObjects, new Vector3(-1f, ySpawnPos+ 1f, zSpawnPos), enemyObjects.rotation); ;
        }
        if (randNumber == 5)
        {
            Instantiate(enemyObjects, new Vector3(0f, ySpawnPos+1f, zSpawnPos), enemyObjects.rotation);
        }
    }
}
