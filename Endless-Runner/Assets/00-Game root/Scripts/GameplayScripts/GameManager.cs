﻿using System.Collections;
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

    enum Levels { endless, one, two, three }
    [SerializeField] static Levels _currentLevel;

    Material[] environmentMaterial = new Material[4];

    int spawnPoint = 12;

    Transform[] Boss = new Transform[3];

    bool _spawnBoss, _spawnPlatform, _spawnActive, _changedlevel, _activateLevel, _displayingUI;

    GameObject World;

    // Start is called before the first frame update
    void Awake()
    {
        _activateLevel = false;
        _changedlevel = false;
        _bossMode = false;
        _spawnBoss = false;
        _currentLevel = Levels.one;
        characterDeath = false;
        _spawnPlatform = true;
        _spawnActive = true;
        _displayingUI = false;

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
    }

    private void FixedUpdate()// call the boss here!!!
    {
        
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
            if (!_displayingUI)
            {
                UI.PlayerDeath();
                _displayingUI = true;
            }
           
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
        spawnPoint += 55;
        spawn.SpawnPlatform(spawnPoint, null, 180f);
        spawnPoint += 5;

    }

    void SpawnBoss()
    {
        SpawnStartPlatform();
        _changedlevel = false;
        _bossMode = true;
        Boss =  Resources.LoadAll<Transform>("Prefabs/Boss");
        Instantiate(Boss[0], new Vector3(0, -.7f, spawnPoint + 100.7f), Boss[0].rotation);
        FindObjectOfType<Boss_1_Manager>().GetSpawnPoint(spawnPoint);
        spawnPoint+=97;    
    }




    }//Gamemanager
