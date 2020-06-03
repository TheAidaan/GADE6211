using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_spawner : MonoBehaviour
{
    Transform[] bossWorld = new Transform[4];

    Transform[] gameWorld = new Transform[2];

    Transform[] Trigers = new Transform[4];
    [SerializeField] GameObject cube;

    bool _maySpawnPlatform = true;

    bool _endBoss = false;
    
    void Start()
    {

        bossWorld = Resources.LoadAll<Transform>("Prefabs/Boss/Level 1/World");

        gameWorld[0] = FindObjectOfType<Spawner>().GetWorldBlocks(0);// add ground block to the World array
        gameWorld[1] = FindObjectOfType<Spawner>().GetWorldBlocks(1);// add wall block to the World array; 

        Trigers = Resources.LoadAll<Transform>("Prefabs/Boss/Triggers");

        Instantiate(bossWorld[0], transform.position, bossWorld[0].transform.rotation, transform); // spawn walkway
        Instantiate(bossWorld[1], new Vector3(transform.position.x,transform.position.y-150,transform.position.z), bossWorld[1].transform.rotation, transform); //sapwn cylinder

        float z = transform.position.z - 52;
        for (int x = -1; x < 2; x++) //spawn the boss starters in each lane
        {
            Instantiate(Trigers[0], new Vector3(x, 1, z), Trigers[0].rotation);
            z++;
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
           Instantiate(cube, new Vector3(0, 2, 50.5f), cube.transform.rotation, transform); // spawn obstacle
        }

    }

    public void SpawnBuildingBlocks(int spawnPoint, Transform parent)
    {
        Instantiate(gameWorld[1], new Vector3(-2f, 0, spawnPoint), gameWorld[1].rotation, parent);

        for (int i = -1; i < 2; i++)
        {
            Instantiate(gameWorld[0], new Vector3(i, 0, spawnPoint), gameWorld[0].rotation, parent);
        }

        Instantiate(gameWorld[1], new Vector3(2f, 0, spawnPoint), gameWorld[1].rotation, parent);
    }

    public void SpawnWalkway()
    {
        if (_maySpawnPlatform)
        {
            Instantiate(bossWorld[0], transform.position, Quaternion.Euler(0, 90.97f, 0), transform);
            _maySpawnPlatform = false;
        }
    }

    public void SpawnEscape(int spawnPoint, Transform parent)
    {
        for (int y = spawnPoint; y < (spawnPoint + 3); y++)
        {
            for (int x = -1; x < 2; x++)
            {
                Instantiate(gameWorld[0], new Vector3(x, 0, y), gameWorld[0].rotation, parent);
            }

            Instantiate(gameWorld[1], new Vector3(2, 0, y), gameWorld[1].rotation, parent);
        }

    }
}
