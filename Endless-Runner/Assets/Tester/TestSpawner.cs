using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    Transform[] World = new Transform[4];
    [SerializeField] Transform Player;
    int spawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        World = Resources.LoadAll<Transform>("Prefabs/Test");
    }

    public void SpawnBuildingBlocks(int spawnPoint)
    {
        Instantiate(World[1], new Vector3(-2f, 0, spawnPoint), World[1].rotation);

        for (int i = -1; i < 2; i++)
        {
            Instantiate(World[0], new Vector3(i,0, spawnPoint), World[0].rotation);
        }

        Instantiate(World[1], new Vector3(2f, 0, spawnPoint), World[1].rotation);
    }

    public Transform GetWorldBlocks(int index)
    {
        return World[index];
    }

}
