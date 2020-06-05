using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Spawner : BossSpawner
{
    Transform[] bossWorld = new Transform[4];

    GameObject[] bossObstacles = new GameObject[4];

    bool _maySpawnWalkway = true;

    void Start()
    {
        bossWorld = Resources.LoadAll<Transform>("Prefabs/Boss/Level 1/World");


        Transform walkway  = Instantiate(bossWorld[0], transform.position, transform.rotation, transform); // spawn walkway
        Transform cylinder = Instantiate(bossWorld[1], new Vector3(transform.position.x,transform.position.y-150 ,transform.position.z), bossWorld[1].transform.rotation, transform); //sapwn cylinder

        GetComponent<Boss_1_Manager>().GetGameObjects(cylinder,walkway);


    }

    public void SpawnWalkway()
    {
        if (_maySpawnWalkway)
        {
            Instantiate(bossWorld[0], transform.position, Quaternion.Euler(0, 90.97f, 0), transform);
            _maySpawnWalkway = false;
        }
    }

    public void SpawnObject(GameObject obj)
    {
        int randNumber = Random.Range(-53, -50); // pick random lane
        if (obj != null)
        {
            obj = Instantiate(obj, new Vector3(randNumber + 0.3f, 1, transform.position.z), obj.transform.rotation, transform);
            obj.AddComponent<Boss_1_ObjectDestoryer>();
        }
    }
}
