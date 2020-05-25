using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    CharacterUI characterUI;
    CharacterAbility characterAbility;
    PlayerStatsUI playerStatsUI;

    int  _totalCoins;
    float _totalDistannce;

    Vector3 _startPosition;

    float _power;

    // Start is called before the first frame update
    void Start()
    {
        characterAbility = GetComponentInParent<CharacterAbility>();
        characterUI = GetComponentInChildren<CharacterUI>();

        characterUI.SetGUI();

        _startPosition = transform.position;
        _totalCoins = 0;
        _totalDistannce = 0;

        _power = 0;
    }

    void Update()
    {
        if (!GameManager.characterDeath)
        {
            _totalDistannce = (transform.position.z - _startPosition.z);
            SendDistance((int)_totalDistannce);
        }
    }

    public void IncreaseCoins(int increment)
    {
        _totalCoins += increment;
        characterUI.SetTotCoins(_totalCoins.ToString());
    }

    public void SendDistance(int disatnce)
    {
        characterUI.SetTotDist(disatnce.ToString());
    }

    public void ChangePower(float charge)
    {
        _power += charge;
        characterUI.SetSlider(_power / 100);

        if (_power == 100)
        {
            characterAbility.Charged(true);
        }
    }
    public void SendStats()
    {
        PlayerStatsUI.TotalDistance = (int)_totalDistannce;
    }
}
