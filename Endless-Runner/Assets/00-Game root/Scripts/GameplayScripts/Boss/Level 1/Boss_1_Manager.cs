using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start = 0, One, Two, Three, End }
    static BOSS_1_STAGES _currrentStage;

    public static int currrentStage { get { return (int)_currrentStage; } }

    Boss_1_spawner spawn;
    Spawner spawner;

    int _spawnPoint; 

    bool gotPlayer = false;

    GameObject empty;

    bool _spawnObject;
    int _spaceBetweenObjects, _minSpaceBetweenObjects ;

     void Awake()
    {
        _minSpaceBetweenObjects = 8;

        _currrentStage = BOSS_1_STAGES.Start;

        spawner = FindObjectOfType<Spawner>();
        spawner.AssignObjects();

        spawn = GetComponent<Boss_1_spawner>();

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.transform.SetParent(transform);
        empty.AddComponent<Boss_1_ObjectDestoryer>();
        spawner.SetParent(empty);

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

                    Transform obj = spawner.PickObject();
                    if (obj != null)
                    {
                        obj.gameObject.AddComponent<Boss_1_ObjectDestoryer>();
                        spawner.SpawnObject(randNumber + 0.3f, obj, false);
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
                if (!endBoss)
                {
                    FetchPlayer();
                }
            }
        }else { BossDeactivation(); }
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(true);
        Player.transform.eulerAngles = new Vector3(0,-82,0);

        spawner.SetParent(gameObject);
        GetSpawnPoint((int)transform.position.z);
        spawner.SetSpawnPoint(_spawnPoint);
        spawner.SetLanes(-53);
    }

    void FetchPlayer()
    {
        if ((Player.position.z > (_spawnPoint - 15)) && (!gotPlayer))
        {
            spawner.SpawnBuildingBlocks(_spawnPoint,null);

            _spawnPoint++;

            if (_spawnPoint == ((int)(transform.position.z - 52.98)))
            {
                spawner.SpawnEscape(_spawnPoint, empty.transform, true);
                gotPlayer = true;
            }
        }
    }

    public void GetSpawnPoint(int spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }


    public override void BossEnd()
    {
        Player.transform.eulerAngles = new Vector3(0, 90, 0);
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(true);
    }

    public void IncreaseStage()
    {
        _currrentStage++;
        Debug.Log(currrentStage);
    }

    public void ReleasePlayer()
    {
        spawner.SpawnEscape(_spawnPoint, transform, false);
    }
}
