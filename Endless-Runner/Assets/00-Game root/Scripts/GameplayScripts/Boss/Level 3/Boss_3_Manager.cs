using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Manager : BossManager
{
    bool  _mayAnimate, _switchRotation;

    Boss_3_Animator animator;

    Boss_3_CharacterMovement playerMovement;

    
    float _rotationSpeed;

    int _playerRotations, _rotationsNeededForNextStage;

    static bool _clockwiseRotation;
    public static bool ClockwiseRotation { get { return _clockwiseRotation; } }


    public override void Start()
    {
        
        base.Start();
        animator = GetComponentInChildren<Boss_3_Animator>();

        gameSpawner.SetSpawnPoint(spawnPoint);

        gameSpawner.ForceBreak(2, 47);

        _rotationSpeed = 1;
        _playerRotations = 0;
        _rotationsNeededForNextStage = 3;

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

                animator.SetTriggers();

                playerMovement.TurnAround();
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
        Destroy(playerMovement);
        Player.transform.eulerAngles = new Vector3(0, 0, 0);
        
        Player.GetComponent<CharacterMovement>().StopForwardMovement(false, true);
        Destroy(gameObject);
        
        base.DeactivateBoss();
    }

    public void MayAnimate()// the player triggers this class
    {
       
        if (CurrrentStage == 2)
        {
            animator.ActivatePlatform();
        }else
        {
            _mayAnimate = true; //the animator gets permission to be called
            _playerRotations++;
        }
    }

    public override void IncreaseStage()
    { 
        if ((_playerRotations >= _rotationsNeededForNextStage) && !Boss_3_Animator.Animated)
        { 
            base.IncreaseStage();

            if (CurrrentStage == 2)
            {
                EndBoss();

                if (_clockwiseRotation)
                {
                    animator.FinalAnimation();
                }else
                {
                    _switchRotation = true;
                }
                
            }
            else
            {
                _rotationsNeededForNextStage += 3;
                _switchRotation = true;
            }    
        }
    }

    public override void EndBoss()
    {
        if (_clockwiseRotation)
        {
            animator.FinalAnimation();
        }
        else
        {
            _switchRotation = true;
        }

        base.EndBoss(); 
    }
}
