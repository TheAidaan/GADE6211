using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    GameManager GM;

    Transform[] bossPrefabs = new Transform[4];

    bool platformSpawned;
   
    void Start()
    {
        GM = GetComponent<GameManager>();
        bossPrefabs = Resources.LoadAll<Transform>("Prefabs/Boss");

        platformSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GM.ActivateBoss();
        }
    }

    public void SpawnPlatform(int spawnPoint,Transform Fling)
    {
        
        if (!platformSpawned)
        {
            Instantiate(bossPrefabs[0], new Vector3(0f, 0f, spawnPoint+2), bossPrefabs[0].rotation);
            Instantiate(Fling, new Vector3(0f, 1f, spawnPoint + 2), Fling.rotation);
            platformSpawned = true;
        }
        
    }
}
