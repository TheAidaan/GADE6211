using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Manager : BossManager
{
    bool  _maySpawnObjects,_spawnObject;

    GameObject empty;

    public override void Start()
    {
        base.Start();
        _spawnObject = true;
        _maySpawnObjects = true;

        gameSpawner.SetSpawnPoint(spawnPoint);

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        gameSpawner.SetParent(empty);

        gameSpawner.ForceBreak(2, 47);


    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                //transform.Rotate(0, -0.29f, 0);

                if (_maySpawnObjects)
                {
                    if (_spawnObject)  
                    {
                
                    }
                }
            }
            else
            {
                if (CurrrentStage == 0)
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
        Player.GetComponent<CharacterMovement>().SetLane(3);
        Player.transform.eulerAngles = new Vector3(0,90,0); 

        gameSpawner.SetSpawnPoint(spawnPoint);
        gameSpawner.SetLanes(-53, 0.33f);
    }


    public override void DeactivateBoss()
    {
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);

        base.DeactivateBoss();
    }

    //public void ReleasePlayer()
    //{
    //    spawn.ReleasePlayer();
    //    _maySpawnObjects = false;
    //}
    //public override void IncreaseStage()
    //{ 
    //    _coolOffTime /= 2;
    //    base.IncreaseStage();
    //}

    //IEnumerator CoolOff()
    //{
    //    yield return new WaitForSeconds(_coolOffTime);
    //    _spawnObject = true;
    //}
}
