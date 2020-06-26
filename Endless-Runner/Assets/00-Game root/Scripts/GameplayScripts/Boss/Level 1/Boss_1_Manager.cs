using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start = 0, One, Two, Three, End }
    static BOSS_1_STAGES _currrentStage;

    public static int CurrrentStage { get { return (int)_currrentStage; } }

    Boss_1_Spawner spawn;
    Spawner gameSpawner;

    bool gotPlayer, _maySpawnObjects,_spawnObject;

    GameObject empty;

    float _coolOffTime;

     void Awake()
    {
        _spawnObject = true;
         gotPlayer = false;
        _maySpawnObjects = true;
        _coolOffTime = 1f;

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
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                transform.Rotate(0, -0.29f, 0);

                if (_maySpawnObjects)
                {
                    if (_spawnObject)  
                    {
                        int randNumber = Random.Range(-53, -50);

                        Transform obj = gameSpawner.PickObject();
                        if (obj != null)
                        {
                            if (obj.gameObject.GetComponent<Boss_1_OjectController>() == null)
                            {
                                
                                obj.gameObject.AddComponent<Boss_1_OjectController>();
                            }
                            spawn.Attack();
                            gameSpawner.SpawnObject(randNumber, obj, false);
                        }
                        _spawnObject = false;
                        StartCoroutine(CoolOff());
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
            transform.Translate(Vector3.down * 3f);
            if (transform.position.y<-50)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().StopForwardMovement(true, true);
        Player.transform.eulerAngles = new Vector3(0,-90,0); 

        GetSpawnPoint((int)transform.position.z);

        gameSpawner.SetSpawnPoint(spawnPoint);
        gameSpawner.SetLanes(-53, 0.33f);

        spawn.SetLanes((int)transform.position.z - 52 , 0);
        spawn.SetSpawnPoint(Player.transform.position.x);
    }

    void FetchPlayer()
    { 
        if ((Player.position.z > (spawnPoint - 15)) && (!gotPlayer))
        {
            gameSpawner.SpawnBuildingBlocks(spawnPoint,null);

            spawnPoint++;

            if (spawnPoint == ((int)(transform.position.z - 51.98)))
            {
                //spawn.SetSpawnPoint(spawnPoint+1);
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
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);

        base.DeactivateBoss();
    }

    public void IncreaseStage()
    {
        _currrentStage++;
        _coolOffTime /= 2;

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

    IEnumerator CoolOff()
    {
        yield return new WaitForSeconds(_coolOffTime);
        _spawnObject = true;
    }
}
