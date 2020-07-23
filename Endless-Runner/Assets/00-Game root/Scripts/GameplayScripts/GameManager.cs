using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool characterDeath;

    static  bool _bossMode;
    public static bool BossMode { get { return _bossMode; } }

    static int _destroyDist;
    public static int DestroyDist { get { return _destroyDist; } }
    Spawner spawn;
    UIManager UI;

    float _waitToLoad = 0;

    public Transform Character;
    public static Transform Player;

    enum Levels { one=1, two, three }
    static Levels _currentLevel;
    public static int CurrentLevel { get { return (int)_currentLevel; } }

    Material[] environmentMaterial = new Material[4];

    int spawnPoint = 12;

    Transform[] Boss = new Transform[3];

    bool _spawnBoss, _transitionToBoss, _spawnActive, _changedlevel, 
        _activateLevel, _displayingUI, _clearPath, _disablingSpawner,
        _randomizeLevels;

    int _clearDist;

    GameObject World;

    // Start is called before the first frame update
    void Awake()
    {
        characterDeath = false;
        _bossMode = false;
        _currentLevel = Levels.three;
        _destroyDist = 6;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
        spawn = GetComponent<Spawner>();
        UI = GetComponentInChildren<UIManager>();
        UI.Begin();

        World = new GameObject();
        spawn.SetParent(World);
        spawn.SetLanes(-1);
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
                            _transitionToBoss = true;
                        }
                    }
                }

                if (_transitionToBoss)
                {
                    spawn.SpawnPlatform(spawnPoint, true);
                    _transitionToBoss = false;
                }

            }
        }

        if (characterDeath)
        {
            _waitToLoad += Time.deltaTime;
        }

        if (_waitToLoad > 2)
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
        
        if ((_currentLevel == Levels.three) && (!_randomizeLevels))
        {
            _randomizeLevels = true;
        }

        if (_randomizeLevels)
        {
            _currentLevel = (Levels)Random.Range(1, 3);
        }
        else
        {
            _currentLevel++;
        }
        
        _changedlevel = true;
        RenderSettings.skybox = environmentMaterial[CurrentLevel];
        _activateLevel = true;
    }

    void ActivateLevel()
    {
        _destroyDist = 6;
        spawn.SetLanes((int)Player.transform.position.x - 1);
        SpawnStartPlatform();

        _spawnActive = true;
        spawn.SetParent(World);
        spawn.SetSpawnPoint(spawnPoint);
        spawn.AssignObjects();
        UI.ShowDistance(true);

        Player.GetComponent<CharacterMovement>().ResetSpeed();


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
            ActivateLevel();
        }

    }

    void SpawnStartPlatform()
    {
        spawnPoint += 54;
        spawn.SpawnPlatform(spawnPoint, false);
        spawnPoint += 5;

    }

    void SpawnBoss()
    {
        SpawnStartPlatform();
        _changedlevel = false;
        _bossMode = true;
        Boss =  Resources.LoadAll<Transform>("Prefabs/Boss");

        Vector3 bossSpawnPos = Vector3.zero;

        switch (CurrentLevel)
        {
            case 1:
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, -.7f, spawnPoint + 100.7f);
                break;
            case 2:
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, Spawner.WorldHeight+6, spawnPoint-2);
                break;
            default:
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, -15, spawnPoint + 100.7f);
                break;
        }

        Instantiate(Boss[CurrentLevel-1], bossSpawnPos, Boss[CurrentLevel-1].rotation);
        FindObjectOfType<BossManager>().GetSpawnPoint(spawnPoint);
        spawnPoint+=97;
        UI.ShowDistance(false);
    }

    public void ChangeDestroyDistance(int DestroyDist)
    {
        _destroyDist = DestroyDist;
    }

    }//Gamemanager
