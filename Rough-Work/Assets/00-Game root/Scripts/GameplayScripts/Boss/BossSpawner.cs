using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public int firstLane, spawnPoint;
    public float offset;
    public int randLane;

    public void SetSpawnPoint(int SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }
    public void SetLanes(int FirstLane, float Offset)
    {
        firstLane = FirstLane;
        offset = Offset;
    }
}
