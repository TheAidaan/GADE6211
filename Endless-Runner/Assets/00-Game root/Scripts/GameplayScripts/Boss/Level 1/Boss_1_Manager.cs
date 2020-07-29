using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Manager : BossManager
{
    Boss_1_PathController _path;
    Boss_1_CharacterMovement playerMovement;
    Boss_1_SpawnPositions _pos;

    Transform _obj;

    bool _maySpawnObjects,_spawnObject;

    public override void Start()
    {
        base.Start();
        _spawnObject = true;
        _maySpawnObjects = true;

        _path = GetComponentInChildren<Boss_1_PathController>();
        _pos = GetComponentInChildren<Boss_1_SpawnPositions>();


    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if (_maySpawnObjects)
                {
                    
                    _obj = gameSpawner.PickObject();
                    Debug.Log(_obj);
                    if (_obj = null)
                    {
                        Instantiate(_obj, _pos.Spawnposition(), _obj.rotation);
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

        _maySpawnObjects = true;
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
