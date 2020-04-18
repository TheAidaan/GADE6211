using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool maySpawnObstacles;

    public static bool characterDeath;
    float waitToLoad = 0;

    Spawner spawn;
    public Transform Character;
    public static Transform Player;

    enum Levels { one = 1, two, three }
    [SerializeField] Levels currentLevel;



    int spawnPoint = 12;

    // Start is called before the first frame update
    void Awake()
    {
    
        characterDeath = false;
        maySpawnObstacles = true;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        spawn = FindObjectOfType<Spawner>();
        spawn.SetLevel((int)currentLevel);
        spawn.AssignObjects();

    }
    void Start()
    {

        for (int spawnPoint = 0; spawnPoint < 12; spawnPoint++)
        {
            spawn.SpawnBuildingBlocks(spawnPoint, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (characterDeath == false)
        {
            if (Player.position.z > (spawnPoint - 15))
            {
                if (maySpawnObstacles == true)
                {
                    spawn.SpawnBuildingBlocks(spawnPoint, spawn.spawnObject());
                }
                else
                {
                    spawn.SpawnBuildingBlocks(spawnPoint, null);
                }

                spawnPoint++;
            }
        }


        if (characterDeath == true)
        {
            waitToLoad += Time.deltaTime;
        }

        if (waitToLoad > 2)
        {
            SceneManager.LoadScene("GameOverMenu");
        }

    }

    public int CurrentLevel()
    {
        return (int)currentLevel;
    }

}//Gamemanager
