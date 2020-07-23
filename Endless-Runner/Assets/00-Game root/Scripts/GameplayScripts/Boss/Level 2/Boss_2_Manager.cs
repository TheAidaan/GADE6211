using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Manager : BossManager
{
    Rigidbody rb;
    Boss_2_Animator animator;

    bool _moveForward,_maySpawn;

    float _forwardSpeed, _totalDistance, _startPosition;
    int _increaseStagepoint;

    Transform[] Triggers;

    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Boss_2_Animator>();
        rb = GetComponent<Rigidbody>();

        _startPosition = transform.position.z;
        _increaseStagepoint = 50;
        _maySpawn = true;

        Triggers = Resources.LoadAll<Transform>("Prefabs/Boss/Level 2/Triggers");
        Instantiate(Triggers[0], new Vector3(Spawner.FirstLane + 1, Spawner.WorldHeight+1, spawnPoint), Triggers[0].rotation);

    }
    private void FixedUpdate()
    {
        if (bossActive)
        {
            rb.velocity = Vector3.forward * _forwardSpeed;
        }
    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if (Player.transform.position.z > (spawnPoint - 5))
                {
                    _totalDistance = (transform.position.z - _startPosition);

                    if (_totalDistance > _increaseStagepoint)
                    {
                        IncreaseStage();
                        _increaseStagepoint += 50;
                    }
                    if(_maySpawn)
                    {
                        gameSpawner.SpawnBuildingBlocks(spawnPoint, null);
                        spawnPoint++;
                    }
                    
                }
            }else
            {
                if (transform.position.z > (spawnPoint - 13))
                {
                    gameSpawner.SpawnBuildingBlocks(spawnPoint, null);
                    spawnPoint++;
                }

                if (transform.position.z <= Player.transform.position.z - 7)
                {
                    BossActivation();
                }
            }    
        }
    }

    public override void ActivateBoss()
    {
        _forwardSpeed = Player.GetComponent<CharacterMovement>().CurrentSpeed();
        Player.GetComponent<CharacterMovement>().InvertInput();
        _moveForward = true;
        animator.isRunning();

        FindObjectOfType<GameManager>().ChangeDestroyDistance(15);    
    }

    public override void EndBoss()
    {
        gameSpawner.SpawnPlatform(spawnPoint, true);
        _maySpawn = false;
        Transform[] ActivationTriggers = Resources.LoadAll<Transform>("Prefabs/Boss/Triggers");

        for(int i = 0; i < 3; i++)
        {
            Instantiate(ActivationTriggers[0], new Vector3(Spawner.FirstLane + i, Spawner.WorldHeight + 1, spawnPoint+1), ActivationTriggers[0].rotation);
        }
        
        base.EndBoss();
    }

    public override void DeactivateBoss()
    {
        base.DeactivateBoss();
        FindObjectOfType<CameraBehaviour>().Boss_2_CameraReset();
        Player.GetComponent<CharacterMovement>().InvertInput();
        Destroy(gameObject);
    }
        

}
