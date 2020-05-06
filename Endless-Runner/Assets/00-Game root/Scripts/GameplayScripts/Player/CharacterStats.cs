using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    CharacterUI characterUI;
    CharacterAbility characterAbility;

    int  coinTotal;
    float distTotal;

    float _power;

    // Start is called before the first frame update
    void Start()
    {
        characterUI = GetComponentInChildren<CharacterUI>();
        characterUI.SetGUI();

        characterAbility = GetComponentInParent<CharacterAbility>();

        coinTotal = 0;
        distTotal = 0;

        _power = 0;
    }

    public void IncreaseCoins(int increment)
    {
        coinTotal += increment;
        characterUI.SetTotCoins(coinTotal.ToString());
    }

    public void IncreaseDistance(int increment)
    {
        distTotal += increment;
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
        GUI.CoinsTot = coinTotal;
        GUI.DisTot = distTotal;
    }
}
