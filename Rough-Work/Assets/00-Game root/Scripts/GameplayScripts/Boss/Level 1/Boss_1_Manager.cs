﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start = 0, One, Two, Three, End }
    static BOSS_1_STAGES _currrentStage;

    public static int CurrrentStage { get { return (int)_currrentStage; } }

    Boss_1_Spawner spawn;
    Spawner gameSpawner;

    bool gotPlayer, _maySpawnObjects;

    GameObject empty;

    bool _spawnObject, _playerInTower;
    int _spaceBetweenObjects, _minSpaceBetweenObjects ;

     void Awake()
    {
        _playerInTower = false;
        gotPlayer = false;
        _maySpawnObjects = true;
        _minSpaceBetweenObjects = 8;

        _currrentStage = BOSS_1_STAGES.Start;

        gameSpawner = FindObjectOfType<Spawner>();
        gameSpawner.AssignObjects();

        spawn = GetComponent<Boss_1_Spawner>();
        spawn.SetLanes(-1,0);
        gameSpawner.SetSpawnPoint(spawnPoint);

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.AddComponent<Boss_1_OjectController>();
        gameSpawner.SetParent(empty);

    }
    void FixedUpdate()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                transform.Rotate(0, -0.29f, 0);

                if (_maySpawnObjects)
                {
                    if ((_spawnObject) && (_spaceBetweenObjects > _minSpaceBetweenObjects))  // turn into time-Based
                    {
                        int randNumber = Random.Range(-53, -50);

                        Transform obj = gameSpawner.PickObject();
                        if (obj != null)
                        {
                            if (obj.gameObject.GetComponent<Boss_1_OjectController>() == null)
                            {
                                obj.gameObject.AddComponent<Boss_1_OjectController>();
                            }
                           
                            gameSpawner.SpawnObject(randNumber, obj, false);
                        }

                        _spawnObject = false;
                        _spaceBetweenObjects = 0;
                    }
                    else
                    {
                        _spawnObject = true;
                        _spaceBetweenObjects += 1;
                    }
                }
            }
            else
            {
                if (_currrentStage == BOSS_1_STAGES.Start)
                {
                    FetchPlayer();
                }
            }
        }else
        {
            base.DeactivateBoss();
        }

       
        if (!GameManager.BossMode)
        {
            transform.Translate(Vector3.down * 1f);
            if (transform.position.y<-50)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().StopForwardMovement(true);
        Player.transform.eulerAngles = new Vector3(0,-90,0);

        GetSpawnPoint((int)transform.position.z);

        gameSpawner.SetSpawnPoint(spawnPoint);
        gameSpawner.SetLanes(-53, 0.33f);

        //switch (CharacterMovement.currentLane)
        //{
        //    case 2:
        //        spawn.SetLanes((int)Player.position.x - 3, 0);
        //        break;
        //    case 3:
        //        spawn.SetLanes((int)Player.position.x - 2, 0);
        //        break;
        //    default: 
        //        spawn.SetLanes((int)Player.position.x, 0);
        //        break;
        //}
        spawn.SetLanes((int)transform.position.z - 52 , 0);
        spawn.SetSpawnPoint((int)transform.position.x);
    }

    void FetchPlayer()
    { 
        if ((Player.position.z > (spawnPoint - 15)) && (!gotPlayer))
        {
            gameSpawner.SpawnBuildingBlocks(spawnPoint,null);

            spawnPoint++;

            if (spawnPoint == ((int)(transform.position.z - 51.98)))
            {
                spawn.SetSpawnPoint(spawnPoint+1);
                gotPlayer = true;
            }
        }
    }

    public void GetSpawnPoint(int SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }


    public override void DeactivateBoss()
    {
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false);

        base.DeactivateBoss();
    }

    public void IncreaseStage()
    {
        _currrentStage++;
        Debug.Log(CurrrentStage);

        if (_currrentStage == BOSS_1_STAGES.End)
        {
            EndBoss();
        }
    }

    public void ReleasePlayer()
    {
        spawn.ReleasePlayer();
        _maySpawnObjects = false;
    }
}