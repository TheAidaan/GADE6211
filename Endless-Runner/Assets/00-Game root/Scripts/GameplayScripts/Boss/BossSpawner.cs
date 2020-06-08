using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform[] bossTrigers = new Transform[4];

    int _firstLane, _spawnPoint;
    float _offset;
    // Start is called before the first frame update
    void Awake()
    {
        bossTrigers = Resources.LoadAll<Transform>("Prefabs/Boss/Triggers");
    }

    public void SpawnActivationTriggers(Transform parent, bool upwards)
    {
        int z = _spawnPoint;
        if (upwards)
        {
            for (int x = _firstLane; x < _firstLane + 3; x++) //spawn the boss starters in each lane
            {
                Instantiate(bossTrigers[0], new Vector3(x+ _offset, 1, z), bossTrigers[0].rotation, parent);// spawns the triggers diagonally
                z++;
            }
        }else
        {
            for (int x = _firstLane+2; x > _firstLane-1; x--) //spawn the boss starters in each lane
            {
                Instantiate(bossTrigers[0], new Vector3(x+ _offset, 1, z), bossTrigers[0].rotation, parent);// spawns the triggers diagonally
                z++;
            }
        }
        
    }

    public void SetSpawnPoint(int spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }
    public void SetLanes(int firstLane, float offset)
    {
        _firstLane = firstLane;
        _offset = offset;
    }
}
