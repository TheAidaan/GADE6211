using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_SpawnPositions : MonoBehaviour
{
    Transform[] _spawnPositions = new Transform[3];

    void Start()
    {
       for (int i = 0; i < 3; i++)
       {
            _spawnPositions[i] = transform.GetChild(i);
       }

    }

    public Vector3 Spawnposition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform.position ;
    }
    public Vector3 Rotation()
    {
        return transform.rotation.eulerAngles;
    }
    public Vector3 MiddleLane()
    {
        return _spawnPositions[1].transform.position;
    }
}
