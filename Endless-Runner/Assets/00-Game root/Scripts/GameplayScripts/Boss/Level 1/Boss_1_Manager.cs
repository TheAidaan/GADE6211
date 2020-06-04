using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    enum BOSS_1_STAGES { Start, One, Two, Three, End }
    BOSS_1_STAGES currrentStage;
    Boss_1_spawner spawn;

    [SerializeField] int _spawnPoint; //MAKE PRIVATE WHEN DONE!

    bool gotPlayer = false;

    GameObject empty;

    public override void Start()
    {
 
        currrentStage = BOSS_1_STAGES.Start;
        spawn = GetComponent<Boss_1_spawner>();

        base.Start();

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        empty.transform.SetParent(transform);
        empty.AddComponent<Boss_1_ObjectDestoryer>();
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
            spawn.SpawnObject(FindObjectOfType<Spawner>().GetSpecificObject(0,0).gameObject);
        }
    }

    public override void BossStart()
    {
        Player.GetComponent<CharacterMovement>().EffectForwardMovement(true);
        Player.transform.eulerAngles = new Vector3(0,-82,0);
    }

    void FetchPlayer()
    {
        
        if ((Player.position.z > (_spawnPoint - 15)) && (!gotPlayer))
        {
            spawn.SpawnBuildingBlocks(_spawnPoint,empty.transform);

            _spawnPoint++;


            if (_spawnPoint == ((int)(transform.position.z - 52.98)))
            {
                spawn.SpawnEscape(_spawnPoint, empty.transform);
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
