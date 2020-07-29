using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_SpawnPositions : MonoBehaviour
{
    Transform[] _spawnPositions = new Transform[3];

    void Start()
    {
        _spawnPositions = GetComponentsInChildren<Transform>();

    }

    public Vector3 Spawnposition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform.position ;
    }
}
