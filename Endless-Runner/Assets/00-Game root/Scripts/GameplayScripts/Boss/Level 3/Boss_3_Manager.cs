using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Manager : BossManager
{
    bool  _maySpawnObjects,_spawnObject;

    GameObject empty;
    Boss_3_Animations animator;

    bool _safeToRaisePlatform;

    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Boss_3_Animations>();

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
        if (_safeToRaisePlatform)
        {
            animator.NormRaiseQ(1);
        }
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                transform.Rotate(Vector3.up, 1);

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
            Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);
            Destroy(Player.GetComponent<Boss_3_CharacterMovement>());
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

        Player.gameObject.AddComponent<Boss_3_CharacterMovement>();
    }


    public override void DeactivateBoss()
    {
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);

        base.DeactivateBoss();
    }

    public void SafeToRaisePlatform()
    {
        _safeToRaisePlatform = !_safeToRaisePlatform;
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
