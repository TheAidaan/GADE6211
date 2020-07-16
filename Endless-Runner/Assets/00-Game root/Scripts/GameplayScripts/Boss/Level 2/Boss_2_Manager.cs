using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Manager : BossManager
{
    Rigidbody rb;
    Boss_2_Animator animator;

    bool _moveForward;

    float _forwwardSpeed;

    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Boss_2_Animator>();
        rb = GetComponent<Rigidbody>();

        Transform[] ChangeCameraTrigger = Resources.LoadAll<Transform>("Prefabs/Boss/Level 2/Triggers");
        Instantiate(ChangeCameraTrigger[0], new Vector3(Spawner.FirstLane + 1, Spawner.WorldHeight+1, spawnPoint), ChangeCameraTrigger[0].rotation);

    }
    private void FixedUpdate()
    {
        if (bossActive)
        {
            rb.velocity = Vector3.forward * _forwwardSpeed;
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
                    gameSpawner.SpawnBuildingBlocks(spawnPoint, null);
                    spawnPoint++;
                }
            }else
            {
                if (transform.position.z > (spawnPoint - 13))
                {
                    gameSpawner.SpawnBuildingBlocks(spawnPoint, null);
                    spawnPoint++;
                }
            }


            if(transform.position.z <= Player.transform.position.z-7)
            {
                BossActivation(); 
            }
    
        }
    }

    public override void ActivateBoss()
    {
        _forwwardSpeed = Player.GetComponent<CharacterMovement>().CurrentSpeed();
        Player.GetComponent<CharacterMovement>().InvertInput();
        _moveForward = true;
        animator.isRunning();

        FindObjectOfType<GameManager>().ChangeDestroyDistance(15);    

    }

}
