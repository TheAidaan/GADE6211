using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool characterDeath;

    public static int CurrentLevel { get { return (int)_currentLevel; } }

    Spawner spawn;
    BossManager BM;

    float waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { endless, one, two, three }
    [SerializeField] static Levels _currentLevel;

    bool _spawnActive;

    int spawnPoint = 12;

    bool _spawnPlatform;

    // Start is called before the first frame update
    void Awake()
    {
        characterDeath = false;
        _spawnPlatform = true;
        _spawnActive = true;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        spawn = GetComponent<Spawner>();
        BM = GetComponent<BossManager>();

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
        if (_spawnActive)
        {
            if (!characterDeath)
            {
                if (Player.position.z > (spawnPoint - 15))
                {
                    spawn.SpawnBuildingBlocks(spawnPoint, spawn.PickObject());
                    spawnPoint++;
                }
            }
        }else
        {
            if(_spawnPlatform)
            {
                spawn.SpawnPlatform(spawnPoint, spawn.GetSpecificObject(2, 3), 0f);
                _spawnPlatform = false;
            }
            
        }

        if (characterDeath)
        {
            waitToLoad += Time.deltaTime;
        }

        if (waitToLoad > 2)
        {
            SceneManager.LoadScene("GameOverMenu");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableSpawner();
        }
    }

    public void DisableSpawner()
    {
        _spawnActive = false;
        _spawnPlatform = true;
    }

    public void ChangeLevel()
    {
        spawnPoint += 56;
        spawn.SpawnPlatform(spawnPoint, null,180f);
        _spawnActive = true;
        spawnPoint += 5;

        _currentLevel++;
 
    }

}//Gamemanager
