using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    Boss_1_PathController _path;
    Boss_1_CharacterMovement playerMovement;
    Circle_SpawnPositions _pos;

    Transform _obj;


    public override void Start()
    {
        base.Start();

        coolOffTime = 0.4f;

        _path = GetComponentInChildren<Boss_1_PathController>();
        _pos = GetComponentInChildren<Circle_SpawnPositions>();
    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if ((mayAttack) && (!stopAttacking))
                {

                    _obj = gameSpawner.PickObject();
                    
                    if (_obj != null)
                    {
                        if (_obj.gameObject.name == "3.Stump")
                        {
                            Instantiate(_obj, _pos.MiddleLane(), Quaternion.Euler(_pos.Rotation()));
                        }else
                        {
                            Instantiate(_obj, _pos.Spawnposition(), Quaternion.Euler(_pos.Rotation()));
                        }
                        mayAttack = false;
                        StartCoroutine(CoolOff());
                    }
                }
            }
            else
            {
                if (CurrrentStage == 1)
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

        mayAttack = true;
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
        stopAttacking = true;
    }
    public override void IncreaseStage()
    { 
        base.IncreaseStage();
    }
}
