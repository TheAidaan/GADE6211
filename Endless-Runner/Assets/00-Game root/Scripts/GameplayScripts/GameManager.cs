using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool maySpawnObstacles;
    public static bool characterDeath;

    Spawner spawn;
    BossManager BM;

    float waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { endless, one, two, three }
    [SerializeField] Levels Level;

    bool BossActive;

    int spawnPoint = 12;

    // Start is called before the first frame update
    void Awake()
    {

        characterDeath = false;
        maySpawnObstacles = true;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        spawn = GetComponent<Spawner>();
        BM = GetComponent<BossManager>();

        BossActive = false;

        SetLevel();
        spawn.AssignObjects();

    }
    void Start()
    {

        for (int spawnPoint = -1; spawnPoint < 12; spawnPoint++)
        {
            spawn.SpawnBuildingBlocks(spawnPoint, null);
        }
    }

    void Update()
    {
        if (!BossActive)
        {
            if (!characterDeath)
            {
                if (Player.position.z > (spawnPoint - 15))
                {
                    if (maySpawnObstacles == true)
                    {
                        spawn.SpawnBuildingBlocks(spawnPoint, spawn.PickObject());
                    }
                    else
                    {
                        spawn.SpawnBuildingBlocks(spawnPoint, null);
                    }

                    spawnPoint++;
                }
            }
        }else
        {
            BM.SpawnPlatform(spawnPoint, spawn.GetSpecificObject(2,2));
        }


        if (characterDeath)
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
        //Player.GetComponent<CharacterAbility>().SetLevel((CurrentLevel()));
        spawn.currentLevel = CurrentLevel();
    }

    public void ActivateBoss()
    {
        BossActive = true;
    }

}//Gamemanager
