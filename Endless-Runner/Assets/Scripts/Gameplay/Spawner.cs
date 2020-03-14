using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform bbNoPit;
    [SerializeField] Transform bbPit;

    [SerializeField] Transform enemyObjects;
    [SerializeField] Transform collectorsObjects;
    [SerializeField] Transform powerUpObjects;

    public float playerPosZ;
    float zSpawnPos;


    public int randNumber;

     public void SpawnBuildingBlocks()
    {
        if (zSpawnPos <= (playerPosZ + 32f))
        {
            Instantiate(bbNoPit, new Vector3(0, 0, zSpawnPos), bbNoPit.rotation);
            zSpawnPos += 4f;
            SpawnObjects();
        }
    }

   void SpawnObjects()
    {
        randNumber = Random.Range(0, 10);
        if (randNumber < 3)
        {
            Instantiate(collectorsObjects, new Vector3(-1f, 1f, zSpawnPos), collectorsObjects.rotation);
        }
        if (randNumber < 7)
        {
            Instantiate(collectorsObjects, new Vector3(1f, 1f, zSpawnPos), collectorsObjects.rotation);
        }
        if (randNumber == 4)
        {
            Instantiate(enemyObjects, new Vector3(-1f, 1f, zSpawnPos), enemyObjects.rotation); ;
        }
        if (randNumber == 5)
        {
            Instantiate(enemyObjects, new Vector3(0f, 1f, zSpawnPos), enemyObjects.rotation);
        }
    }
}
