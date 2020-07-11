﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Manager : BossManager
{
    bool  _mayAnimate, _switchRotation;

    GameObject empty;
    Boss_3_Animations animator;

    Boss_3_CharacterMovement playerMovement;

    static bool _clockwiseRotation;
    float _rotationSpeed;
    public static bool ClockwiseRotation { get { return _clockwiseRotation; } }


    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Boss_3_Animations>();

        gameSpawner.SetSpawnPoint(spawnPoint);

        empty = new GameObject();
        empty.transform.position = new Vector3(-53, 1, transform.position.z);
        gameSpawner.SetParent(empty);

        gameSpawner.ForceBreak(2, 47);

        _rotationSpeed = 1;



    }
    void Update()
    {
        if (!GameManager.characterDeath)
        {
            if (bossActive)
            {
                if (_clockwiseRotation)
                {
                    transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
                }else
                {
                    transform.Rotate(Vector3.down, _rotationSpeed * Time.deltaTime);
                }
                

                if (_mayAnimate) //can the animator be called?
                {
                   animator.SetAnimationState(); // the animator is called
                   _mayAnimate = false; // the animator looses permission to be called
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
            Destroy(playerMovement);
        }

        if (_switchRotation)
        {
            _rotationSpeed -= Time.deltaTime;
            if (_rotationSpeed < 0)
            {
                _switchRotation = false;
                _clockwiseRotation = !_clockwiseRotation;
                _rotationSpeed = 1;

                playerMovement.TurnAround();


            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _switchRotation = true;
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

        playerMovement = Player.GetComponent<Boss_3_CharacterMovement>();
    }


    public override void DeactivateBoss()
    {
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);

        base.DeactivateBoss();
    }

    public void SafeToRaisePlatform()// the player triggers this class
    {
        _mayAnimate = true; //the animator gets permission to be called
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