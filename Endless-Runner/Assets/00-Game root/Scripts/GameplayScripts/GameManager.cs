using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static bool characterDeath;

    static bool _characterAbility;
    public static bool CharacterAbility { get { return _characterAbility; } }

    static  bool _bossMode;
    public static bool BossMode { get { return _bossMode; } }

    Spawner _spawn;
    GameUI _UI;

    float _waitToLoad = 0;

    Transform _character;
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

    int _clearDist, _bossSpawnPoint,_bossOneSpawned, _bossTwoSpawned, _bossThreeSpawned, _bossesBeaten;

    GameObject World;
    public event Action updateMetrics;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;

        _characterAbility = false;
        characterDeath = false;
        _bossMode = false;
        _currentLevel = Levels.one;

        if (CurrentLevel > 1)
        {
            _characterAbility = true;
        }

        _bossOneSpawned = 0;
        _bossTwoSpawned = 0;
        _bossThreeSpawned = 0;
        _bossesBeaten = 0;

        _bossSpawnPoint = 1000;

        //Instantiate(Character, new Vector3(0, .9f, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
        _spawn = GetComponent<Spawner>();
        _UI = GetComponentInChildren<GameUI>();

        World = new GameObject();
        _spawn.SetWorldHeight(0);
        _spawn.SetParent(World);
        _spawn.SetLanes(-1);
        _spawn.AssignObjects();

        environmentMaterial = Resources.LoadAll<Material>("Materials/World");
        RenderSettings.skybox = environmentMaterial[CurrentLevel-1];
    }

   
   
    void Start()
    {

        for (int spawnPoint = -1; spawnPoint < 12; spawnPoint++)
        {
            _spawn.SpawnBuildingBlocks(spawnPoint, null);
        }
        _spawnActive = true;
    }

    private void FixedUpdate()
    {
        //if (CharacterStats.Distance > _bossSpawnPoint)
        //{
            if (!_disablingSpawner)
            {
                DisableSpawner();
                _spawnBoss = true;

            }
        //}

        if (updateMetrics != null)
        {
            updateMetrics();
        }

    }

    void Update()
    {
        if (!characterDeath)
        {
            if (_spawnActive)
            {
              
               if (Player.position.z > (spawnPoint - 15))
               {
                    _spawn.SpawnBuildingBlocks(spawnPoint, _spawn.PickObject());
                    spawnPoint++;
               }
         
            }
            else
            {
                if (_clearPath)
                {
                    if (Player.position.z > (spawnPoint - 15))
                    {
                        _spawn.SpawnBuildingBlocks(spawnPoint, null);
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
                    _spawn.SpawnPlatform(spawnPoint, true);
                    _transitionToBoss = false;
                }

            }
        }

        if (characterDeath)
        {
            _characterAbility = false;
            _waitToLoad += Time.deltaTime;
        }

        if (_waitToLoad > 2)
        {
            if (!_displayingUI)
            {
                _UI.PlayerDeath();
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
        
        if ((_currentLevel == Levels.three) && (!_randomizeLevels))
        {
            _randomizeLevels = true;
        }

        if (_randomizeLevels)
        {
            _currentLevel = (Levels)UnityEngine.Random.Range(1, 3);
        }
        else
        {
            _currentLevel++;
        }
        if (!_characterAbility)
        {
            _characterAbility = true;
        }
        
        _changedlevel = true;
        RenderSettings.skybox = environmentMaterial[CurrentLevel-1];
        _activateLevel = true;
    }

    void ActivateLevel()
    {

        _spawn.SetLanes((int)Player.transform.position.x-1);
        Player.GetComponent<CharacterMovement>().SetLane(2);
        SpawnStartPlatform();

        _clearPath = true;
        _clearDist = 3;

        _spawnActive = true;
        _spawn.SetParent(World);
        _spawn.SetSpawnPoint(spawnPoint);
        _spawn.AssignObjects();
        _UI.ShowDistance(true);

        Player.GetComponent<CharacterMovement>().ResetSpeed();

        _bossSpawnPoint = CharacterStats.Distance + 1000;


    }

    public void Transition()
    {
        spawnPoint = (int)Player.transform.position.z;
        _spawn.SetWorldHeight((int)Player.transform.position.y);
        
        if (BossMode)
        {    
            _spawnBoss = false;

            if (!_changedlevel)
            {
                ChangeLevel();
            }

            _bossMode = false;
            updateMetrics += updateBossesBeaten;
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
        _spawn.SpawnPlatform(spawnPoint, false);
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
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, Spawner.WorldHeight, spawnPoint + 2);
                updateMetrics += updateBossOneSpawned;
                break;
            case 2:
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, -.7f, spawnPoint + 100.7f);
                updateMetrics += updateBossTwoSpawned;
                break;
            default:
                bossSpawnPos = new Vector3(Spawner.FirstLane + 1, -15, spawnPoint + 100.7f);
                updateMetrics += updateBossThreeSpawned;
                break;
        }

        Instantiate(Boss[CurrentLevel-1], bossSpawnPos, Boss[CurrentLevel-1].rotation);
        FindObjectOfType<BossManager>().GetSpawnPoint(spawnPoint);
        spawnPoint+=97;
        _UI.ShowDistance(false);
    }

    private void updateBossOneSpawned()
    {
        _bossOneSpawned++;
        print("Boss1 has been spawned " + _bossOneSpawned +" times");
        updateMetrics -= updateBossOneSpawned;
    }
    private void updateBossTwoSpawned()
    {
        _bossTwoSpawned++;
        print("Boss2 has been spawned " + _bossTwoSpawned + " times");
        updateMetrics -= updateBossTwoSpawned;
    }
    private void updateBossThreeSpawned()
    {
        _bossThreeSpawned++;
        print("Boss3 has been spawned " + _bossThreeSpawned + " times");
        updateMetrics -= updateBossThreeSpawned;
    }

    private void updateBossesBeaten()
    {
        _bossesBeaten++;
        print("Bosses beaten " + _bossThreeSpawned);
        updateMetrics -= updateBossesBeaten;
    }

}//Gamemanager
