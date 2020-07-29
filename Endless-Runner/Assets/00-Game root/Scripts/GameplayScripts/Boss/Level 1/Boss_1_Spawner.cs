﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Spawner : BossSpawner
{
    Transform[] bossObstacles = new Transform[4];

    //readonly GameObject[] _children = new GameObject[6];

    void Start()
    {
        bossObstacles = Resources.LoadAll<Transform>("Prefabs/Boss/Level 1/Obstacles");

    }

   public  void Attack()
    {
        int randNum = Random.Range(10* Boss_1_Manager.CurrrentStage, 100);

        if (randNum>60)
        {
            randLane = Random.Range(firstLane, firstLane + 3); // pick random lane
            Instantiate(bossObstacles[0], new Vector3(spawnPoint, 10, randLane), bossObstacles[0].transform.rotation);
        }
        
 
    }

    public void SpawnObject(GameObject obj)
    {
        int randNumber = Random.Range(-53, -50); // pick random lane
        if (obj != null)
        {
            obj = Instantiate(obj, new Vector3(randNumber + 0.3f, 1, transform.position.z), obj.transform.rotation, transform);
            obj.AddComponent<Boss_1_ObjectController>();
        }
    }
}
