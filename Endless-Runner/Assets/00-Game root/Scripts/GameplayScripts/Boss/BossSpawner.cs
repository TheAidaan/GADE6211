using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform[] bossTrigers = new Transform[4];

    int firstLane, spawnPoint;
    // Start is called before the first frame update
    void Awake()
    {
        bossTrigers = Resources.LoadAll<Transform>("Prefabs/Boss/Triggers");
    }

    public void SpawnActivationTriggers(Transform parent)
    {
        int z = spawnPoint;
        
            for (int x = firstLane; x < firstLane + 3; x++) //spawn the boss starters in each lane
            {
                Instantiate(bossTrigers[0], new Vector3(x, 1, z), bossTrigers[0].rotation,parent);// spawns the triggers diagonally
                z++;
            }
        
    }

    public void SetSpawnPoint(int SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }
    public void SetLanes(int FirstLane)
    {
        firstLane = FirstLane;
    }
}
