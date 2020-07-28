using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    GameUI UI;
    CharacterAbility characterAbility;

    int  _totalCoins;
    static float _totalDistance;
    public static int Distance { get { return (int)_totalDistance; } }

    Vector3 _startPosition;

    float _power;

    // Start is called before the first frame update
    void Start()
    {
        characterAbility = GetComponentInParent<CharacterAbility>();
        UI = FindObjectOfType<GameUI>();

        _startPosition = transform.position;
        _totalCoins = 0;
        _totalDistance = 0;

        _power = 0;
    }

    void Update()
    {
        if (!GameManager.characterDeath)
        {
            _totalDistance = (transform.position.z - _startPosition.z);
            UI.SetTotDist((int)_totalDistance);
        }
    }

    public void ChangePower(float charge)
    {
        _power += charge;
        UI.SetSlider(_power / 100);

        if (_power == 100)
        {
            characterAbility.Charged(true);
        }
    }
}
