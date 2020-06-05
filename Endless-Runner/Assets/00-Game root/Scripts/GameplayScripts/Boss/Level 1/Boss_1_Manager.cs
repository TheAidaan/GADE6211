using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start = 0, One, Two, Three, End }
    static BOSS_1_STAGES _currrentStage;

    public static int currrentStage { get { return (int)_currrentStage; } }

    Boss_1_Spawner spawn;
    Spawner gameSpawner;

    Transform _cylinder, _walkway;

    int _spawnPoint; 

    bool gotPlayer = false;

    GameObject empty;

    bool _spawnObject;
    int _spaceBetweenObjects, _minSpaceBetweenObjects ;

    Transform[] bossWorldObjects =  new Transform[3];

     void Awake()
    {
        _minSpaceBetweenObjects = 8;

        _currrentStage = BOSS_1_STAGES.Start;

        gameSpawner = FindObjectOfType<Spawner>();
        gameSpawner.AssignObjects();

        spawn = GetComponent<Boss_1_Spawner>();
        spawn.SetLanes(-1);
        gameSpawner.SetSpawnPoint(_spawnPoint);

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.transform.SetParent(transform);
        empty.AddComponent<Boss_1_ObjectDestoryer>();
        gameSpawner.SetParent(empty);

    }
    void FixedUpdate()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                transform.Rotate(0, -0.29f, 0);

                if ((_spawnObject) && (_spaceBetweenObjects > _minSpaceBetweenObjects))  // turn into time-Based
                {
                    int randNumber = Random.Range(-53, -50);

                    Transform obj = gameSpawner.PickObject();
                    if (obj != null)
                    {
                        obj.gameObject.AddComponent<Boss_1_ObjectDestoryer>();
                        gameSpawner.SpawnObject(randNumber + 0.3f, obj, false);
                    }

                    _spawnObject = false;
                    _spaceBetweenObjects = 0;
                }
                else
                {
                    _spawnObject = true;
                    _spaceBetweenObjects+=1;
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
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(true);
        Player.transform.eulerAngles = new Vector3(0,-82,0);

        gameSpawner.SetParent(gameObject);

        GetSpawnPoint((int)transform.position.z);

        gameSpawner.SetSpawnPoint(_spawnPoint);
        gameSpawner.SetLanes(-53);

        spawn.SetLanes(-53);
        spawn.SetSpawnPoint(_spawnPoint);
    }

    void FetchPlayer()
    {
        if ((Player.position.z > (_spawnPoint - 15)) && (!gotPlayer))
        {
            gameSpawner.SpawnBuildingBlocks(_spawnPoint,null);

            _spawnPoint++;

            if (_spawnPoint == ((int)(transform.position.z - 52.98)))
            {
                spawn.SetSpawnPoint(_spawnPoint+1);
                spawn.SpawnActivationTriggers(empty.transform);
                gameSpawner.SpawnEscape(_spawnPoint, empty.transform, true);
                gotPlayer = true;
            }
        }
    }

    public void GetSpawnPoint(int spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }


    public override void DeactivateBoss()
    {

        _cylinder.gameObject.GetComponent<BoxCollider>().isTrigger = false;

        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(false);

        base.DeactivateBoss();
    }

    public void IncreaseStage()
    {
        _currrentStage++;
        Debug.Log(currrentStage);

        if (_currrentStage == BOSS_1_STAGES.End)
        {
            EndBoss();
        }
    }

    public void ReleasePlayer()
    {
        Destroy(_walkway.gameObject);
        spawn.SpawnActivationTriggers(transform);
        gameSpawner.SpawnEscape(_spawnPoint, transform, false);
    }

    public void GetGameObjects(Transform cylinder, Transform walkway)
    {
        _cylinder = cylinder;
        _walkway = walkway;
    }


}
