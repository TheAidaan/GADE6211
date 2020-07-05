using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool characterDeath;
    public static int CurrentLevel { get { return (int)_currentLevel; } }

    static  bool _bossMode;
    public static bool BossMode { get { return _bossMode; } }
    Spawner spawn;
    UIManager UI;

    float waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { one=1, two, three }
    [SerializeField] static Levels _currentLevel;

    Material[] environmentMaterial = new Material[4];

    int spawnPoint = 12;

    Transform[] Boss = new Transform[3];

    bool _spawnBoss, _spawnPlatform, _spawnActive, _changedlevel, _activateLevel, _displayingUI, _clearPath, _disablingSpawner;

    int _clearDist;

    GameObject World;

    // Start is called before the first frame update
    void Awake()
    {
        _activateLevel = false;
        _changedlevel = false;
        _bossMode = false;
        _spawnBoss = false;
        _currentLevel = Levels.three;
        characterDeath = false;
        _spawnPlatform = false;
        _spawnActive = false;
        _displayingUI = false;
        _clearPath = false;
        _disablingSpawner = false;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
        spawn = GetComponent<Spawner>();
        UI = GetComponentInChildren<UIManager>();
        UI.Begin();

        World = new GameObject();
        spawn.SetParent(World);
        spawn.SetLanes(-1,0);
        spawn.AssignObjects();

        environmentMaterial = Resources.LoadAll<Material>("Materials/World");
        RenderSettings.skybox = environmentMaterial[CurrentLevel];

    }
    void Start()
    {

        for (int spawnPoint = -1; spawnPoint < 12; spawnPoint++)
        {
            spawn.SpawnBuildingBlocks(spawnPoint, null);
        }
        _spawnActive = true;
    }

    private void FixedUpdate()
    {
        //if (CharacterStats.Distance > 1000)
        //{
            if (!_disablingSpawner)
            {
                DisableSpawner();
                _spawnBoss = true;

            }

        //}

    }

    void Update()
    {
        if (!characterDeath)
        {
            if (_spawnActive)
            {
               
                    if (Player.position.z > (spawnPoint - 15))
                    {
                        spawn.SpawnBuildingBlocks(spawnPoint, spawn.PickObject());
                        spawnPoint++;
                    }
         
            }
            else
            {
                if (_clearPath)
                {
                    if (Player.position.z > (spawnPoint - 15))
                    {
                        spawn.SpawnBuildingBlocks(spawnPoint, null);
                        spawnPoint++;
                        if (spawnPoint == _clearDist)
                        {
                            _clearPath = false;
                            _spawnPlatform = true;
                        }
                    }
                }

                if (_spawnPlatform)
                {
                    spawn.SpawnPlatform(spawnPoint, spawn.GetSpecificObject(2, 3), 0f);
                    _spawnPlatform = false;
                }

            }
        }

        if (characterDeath)
        {
            waitToLoad += Time.deltaTime;
        }

        if (waitToLoad > 2)
        {
            if (!_displayingUI)
            {
                UI.PlayerDeath();
                _displayingUI = true;
            }

        }
        
    }

    public void DisableSpawner()
    {
        _disablingSpawner = true;
        _spawnActive = false;
        _clearPath = true;
        _clearDist = spawnPoint + 20;

    }

    public void ChangeLevel()
    {
        UI.ShowPowerIndicator(true);
        _changedlevel = true;
        _currentLevel++;
        RenderSettings.skybox = environmentMaterial[CurrentLevel];
        _activateLevel = true;


    }

    void ActivateLevel()
    {
        _spawnActive = true;
        spawn.SetParent(World);
        spawn.SetLanes(-1, 0);
        spawn.SetSpawnPoint(spawnPoint);
        spawn.AssignObjects();
        UI.ShowDistance(true);
    }

    public void Transition()
    {
        spawnPoint = (int)Player.transform.position.z;
        spawn.SetWorldHeight((int)Player.transform.position.y);
        
        if (BossMode)
        {
            
            _spawnBoss = false;

            if (!_changedlevel)
            {
                ChangeLevel();
            }

            _bossMode = false;
        }

        if (_spawnBoss && !BossMode)
        {
            SpawnBoss();
        }

        if (_activateLevel)
        {
            SpawnStartPlatform();
            ActivateLevel();
        }

    }

    void SpawnStartPlatform()
    {
        spawnPoint += 54;
        spawn.SpawnPlatform(spawnPoint, null, 180f);
        spawnPoint += 5;

    }

    void SpawnBoss()
    {
        SpawnStartPlatform();
        _changedlevel = false;
        _bossMode = true;
        Boss =  Resources.LoadAll<Transform>("Prefabs/Boss");

        Vector3 bossPos = Vector3.zero;

        switch (CurrentLevel)
        {
            case 1:
                bossPos = new Vector3(0, -.7f, spawnPoint + 100.7f);
                break;
            case 2:
                break;
            default:
                bossPos = new Vector3(0, -15, spawnPoint + 100.7f);
                break;
        }

        Instantiate(Boss[CurrentLevel-1], bossPos, Boss[CurrentLevel-1].rotation);
        FindObjectOfType<BossManager>().GetSpawnPoint(spawnPoint);
        spawnPoint+=97;
        UI.ShowDistance(false);
    }

    }//Gamemanager
