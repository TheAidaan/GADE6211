using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public int firstLane;
    public float spawnPoint,offset;
    public int randLane;
    public void SetSpawnPoint(float SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }
    public void SetLanes(int FirstLane, float Offset)
    {
        firstLane = FirstLane;
        offset = Offset;
    }
}
