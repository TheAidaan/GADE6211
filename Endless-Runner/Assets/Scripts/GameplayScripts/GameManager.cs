using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Spawner
{
    public static bool maySpawnObstacles;
    public static bool characterDeath;


    float waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { endless, one, two, three }
    [SerializeField] Levels Level;



    int spawnPoint = 12;

    // Start is called before the first frame update
    void Awake()
    {
    
        characterDeath = false;
        maySpawnObstacles = true;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        
        SetLevel();
        AssignObjects();

    }
    void Start()
    {

        for (int spawnPoint = -1; spawnPoint < 12; spawnPoint++)
        {
            SpawnBuildingBlocks(spawnPoint, null);
        }
    }

    void Update()
    {
        if (characterDeath == false)
        {
            if (Player.position.z > (spawnPoint - 15))
            {
                if (maySpawnObstacles == true)
                {
                    SpawnBuildingBlocks(spawnPoint, PickObject());
                }
                else
                {
                    SpawnBuildingBlocks(spawnPoint, null);
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
        return (int)Level;
    }

    void SetLevel()
    {
        Player.GetComponent<CharacterMovement>().SetLevel((CurrentLevel()));
        Player.GetComponent<CharacterPowers>().SetLevel((CurrentLevel()));
        currentLevel = CurrentLevel();
    }

}//Gamemanager
