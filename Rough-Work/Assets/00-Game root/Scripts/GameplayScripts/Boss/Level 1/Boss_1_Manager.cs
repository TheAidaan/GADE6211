using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start, One, Two, Three, End }
    BOSS_1_STAGES currrentStage;
    Boss_1_spawner spawn;
    Spawner spawner;

    [SerializeField] int _spawnPoint; //MAKE PRIVATE WHEN DONE!

    bool gotPlayer = false;

    GameObject empty;

    int _firstLane;

     void Awake()
    {
        currrentStage = BOSS_1_STAGES.Start;

        spawner = FindObjectOfType<Spawner>();
        spawner.AssignObjects();

        spawn = GetComponent<Boss_1_spawner>();


        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.transform.SetParent(transform);
        empty.AddComponent<Boss_1_ObjectDestoryer>();
        spawner.SetParent(empty);

       // _firstLane = (int)(transform.position.x - 53);

    }
    void FixedUpdate()
    {
        if (BossManager.bossActive)
        {
            transform.Rotate(0, -0.29f, 0);
        }
        else
        {
            FetchPlayer();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randNumber = Random.Range(-53, -50);

            Transform obj = spawner.PickObject();
            if(obj!=null)
            {
                obj.gameObject.AddComponent<Boss_1_ObjectDestoryer>();
                spawner.SpawnObject(randNumber + 0.3f, obj, false);
            }
        }
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(true);
        Player.transform.eulerAngles = new Vector3(0,-82,0);

        spawner.SetParent(gameObject);
        spawner.SetSpawnPoint((int)transform.position.z);
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
                spawner.SpawnEscape(_spawnPoint, empty.transform);
                gotPlayer = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currrentStage++;
    }

    public void GetSpawnPoint(int spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

}
