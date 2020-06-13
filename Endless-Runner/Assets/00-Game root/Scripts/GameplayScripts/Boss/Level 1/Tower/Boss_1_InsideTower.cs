using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_1_InsideTower : MonoBehaviour
{
    CanvasGroup CG;
    bool _flash, _playerInPlace;
    int _pressesNeeded, _pressesRecieved;
    void Start()
    {
        _pressesRecieved = 0;
        _pressesNeeded = 4;
        _flash = false;
        _playerInPlace = false;
        CG = GetComponentInChildren<CanvasGroup>();
    }
    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.S)) && (_playerInPlace))
        {
            _pressesRecieved++;
            _flash = true;
            CG.alpha = 1;

            if (_pressesRecieved == _pressesNeeded)
            {
                FindObjectOfType<CharacterReact>().Fling(true);
                FindObjectOfType<GameManager>().Transition();
                FindObjectOfType<CharacterMovement>().StopForwardMovement(false);
                FindObjectOfType<CharacterMovement>().LockControls(false);
            }
        }

        if (_flash)
        {
            CG.alpha = CG.alpha - Time.deltaTime;
            if (CG.alpha <= 0)
            {
                CG.alpha = 0;
                _flash = false;
            }
        }
    }

    public void PlayerIsInPlace( bool playerInPlace)
    {
        _playerInPlace = playerInPlace;

        CG.alpha = 1;
        _flash = true;
        FindObjectOfType<GameManager>().ChangeLevel();
    }


}
