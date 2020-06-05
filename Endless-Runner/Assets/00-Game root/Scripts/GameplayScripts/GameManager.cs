using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool characterDeath;
    public static int CurrentLevel { get { return (int)_currentLevel; } }

    static  bool _bossMode;
    public static bool BossMode { get { return _bossMode; } }
    Spawner spawn;

    float waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { endless, one, two, three }
    [SerializeField] static Levels _currentLevel;

    Material[] environmentMaterial = new Material[4];

    bool _spawnActive;

    int spawnPoint = 12;

    bool _spawnPlatform;

    Transform[] Boss = new Transform[3];

    bool _spawnBoss = false;
    bool _changeLevel = false;

    GameObject World;

    // Start is called before the first frame update
    void Awake()
    {
        _bossMode = false;

        _currentLevel = Levels.one;
        characterDeath = false;
        _spawnPlatform = true;
        _spawnActive = true;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
        spawn = GetComponent<Spawner>();
        World = new GameObject();
        spawn.SetParent(World);
        spawn.SetLanes(-1);
        spawn.AssignObjects();

        environmentMaterial = Resources.LoadAll<Material>("Materials/World");
        StartCoroutine(setEnvironment());

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

        if (Input.GetKeyDown(KeyCode.LeftShift))                //THIS MUST BE IN FIXED UPDATE (Call boss class)
        {
            DisableSpawner();
            _spawnBoss = true;
        }
    }

    public void DisableSpawner()
    {
        _spawnActive = false;
        _spawnPlatform = true;
    }

    public void ChangeLevel()
    {
        spawn.AssignObjects();

        _spawnActive = true;
        _currentLevel++;

        StartCoroutine(setEnvironment());
 
    }

    IEnumerator setEnvironment()
    {   
        yield return new WaitForSeconds(.5f);
        RenderSettings.skybox = environmentMaterial[CurrentLevel];
    }

    public void Transition()
    {
        spawnPoint += 56;
        spawn.SpawnPlatform(spawnPoint, null, 180f);
        spawnPoint += 5;

        if (_spawnBoss)
        {
            SpawnBoss();
        }

        if (_changeLevel)
        {
            ChangeLevel();
        }

    }

    void SpawnBoss()
    {
        _bossMode = true;
        Boss =  Resources.LoadAll<Transform>("Prefabs/Boss");
        Instantiate(Boss[0], new Vector3(0, -.7f, spawnPoint + 100f), Boss[0].rotation);
        FindObjectOfType<Boss_1_Manager>().GetSpawnPoint(spawnPoint);
    }




}//Gamemanager
