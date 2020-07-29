﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    Boss_1_PathController _path;
    Boss_1_CharacterMovement playerMovement;
    Boss_1_Spawner spawn;

    bool _maySpawnObjects,_spawnObject;

    GameObject empty;

    float _coolOffTime;

    public override void Start()
    {
        base.Start();
        _spawnObject = true;
        _maySpawnObjects = true;
        _coolOffTime = 1f;

        _path = GetComponentInChildren<Boss_1_PathController>();

        spawn = GetComponent<Boss_1_Spawner>();
        spawn.SetLanes(-1,0);
        gameSpawner.SetSpawnPoint(spawnPoint);

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.AddComponent<Boss_1_ObjectController>();
        gameSpawner.SetParent(empty);

    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if (_maySpawnObjects)
                {
                    if (_spawnObject)  
                    {
                        //int randNumber = Random.Range(-53, -50);

                        //Transform obj = gameSpawner.PickObject();
                        //if (obj != null)
                        //{
                        //    if (obj.gameObject.GetComponent<Boss_1_ObjectController>() == null)
                        //    {
                                
                        //        obj.gameObject.AddComponent<Boss_1_ObjectController>();
                        //    }
                        //    spawn.Attack();
                        //    gameSpawner.SpawnObject(randNumber, obj, false);
                        //}
                        //_spawnObject = false;
                        //StartCoroutine(CoolOff());
                    }
                }
            }
            else
            {
                if (CurrrentStage == 0)
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
            transform.Translate(Vector3.down * 3f);
            if (transform.position.y<-50)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void ActivateBoss()
    {
        Player.GetComponent<CharacterMovement>().StopForwardMovement(true, true);
        Player.transform.eulerAngles = new Vector3(0,-90,0);

        Player.gameObject.AddComponent<Boss_1_CharacterMovement>();

        playerMovement = Player.GetComponent<Boss_1_CharacterMovement>();

        GetSpawnPoint((int)transform.position.z);

        gameSpawner.SetSpawnPoint(spawnPoint);
        gameSpawner.SetLanes(-53.33f);

        spawn.SetLanes((int)transform.position.z - 52 , 0);
        spawn.SetSpawnPoint(Player.transform.position.x);
    }


    public override void DeactivateBoss()
    {
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(playerMovement);
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);

        base.DeactivateBoss();
    }

    public void ReleasePlayer()
    {
        _path.ReleasePlayer();
        _maySpawnObjects = false;
    }
    public override void IncreaseStage()
    { 
        base.IncreaseStage();
    }
}
