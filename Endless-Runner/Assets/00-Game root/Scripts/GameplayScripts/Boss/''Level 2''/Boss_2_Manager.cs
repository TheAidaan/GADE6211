using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Manager : BossManager
{
    Rigidbody _rb;
    Boss_2_Animator _animator;
    Boss_2_Laser _gun;

    bool _maySpawn,_moveForward, _attacking,_assessing;

    float _forwardSpeed, _totalDistance, _startPosition;
    int _increaseStagepoint;

    public override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Boss_2_Animator>();
        _gun = GetComponentInChildren<Boss_2_Laser>();
        _rb = GetComponent<Rigidbody>();

        _startPosition = transform.position.z;
        _increaseStagepoint = 50;
        _maySpawn = true;
        

    }
    void FixedUpdate()
    {
        if (_moveForward)
        {
            _rb.velocity = Vector3.forward * _forwardSpeed;
        }else
        {
            _rb.velocity = Vector3.zero;
        }
  
    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if (!_attacking)
                {
                    if(!_assessing) // assessing whether to attack or not
                    {
                        StartCoroutine(Attack());
                    }
                    
                }
                

                if (Player.transform.position.z > (spawnPoint - 15))
                {
                    _totalDistance = (transform.position.z - _startPosition);

                    if (_totalDistance > _increaseStagepoint)
                    {
                        IncreaseStage();
                        _increaseStagepoint += 50;
                    }
                    if (_maySpawn)
                    {
                        gameSpawner.SpawnBuildingBlocks(spawnPoint, gameSpawner.PickObject());
                        spawnPoint++;
                    }

                }
            }
            else
            {
                if (transform.position.z > (spawnPoint - 13))
                {
                    gameSpawner.SpawnBuildingBlocks(spawnPoint, null);
                    spawnPoint++;
                }
            }
        }else
        {
            _moveForward = false;
        }
    }

    public override void ActivateBoss()
    {
        _forwardSpeed = Player.GetComponent<CharacterMovement>().CurrentSpeed();
        _gun.Activate(Player);
        _moveForward = true;
        base.ActivateBoss();
    }

    IEnumerator Attack()
    {
        _assessing = true;
        yield return new WaitForSeconds(1f);

        int RandNum = Random.Range(0, 10);

        if (RandNum<4)
        {
            StartCoroutine(_animator.Smash());
            Attacking();
        }
        else
        {
            if (RandNum > 7)
            {
                _animator.Aim();
                StartCoroutine(_gun.Shoot());

                Attacking();
            }
        }
        _assessing = false;
    }
            

    public override void EndBoss()
    {
        gameSpawner.SpawnPlatform(spawnPoint, true);
        _maySpawn = false;
        _moveForward = false;
        Transform[] ActivationTriggers = Resources.LoadAll<Transform>("Prefabs/Boss/Triggers");

        for (int i = 0; i < 3; i++)
        {
            Instantiate(ActivationTriggers[0], new Vector3(Spawner.FirstLane + i, Spawner.WorldHeight + 1, spawnPoint + 1), ActivationTriggers[0].rotation);
        }

        base.EndBoss();
    }

    public override void DeactivateBoss()
    {
        base.DeactivateBoss();
        Destroy(gameObject);
    }

    public void Attacking()
    {
        _attacking = !_attacking;
    }


}
