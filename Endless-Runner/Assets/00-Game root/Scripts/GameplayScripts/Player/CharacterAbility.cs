using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    CharacterStats stats;
    int currentLevel;

    bool _charged = false;
    void Start()
    {
        stats = GetComponentInChildren<CharacterStats>();
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.Q)) && (_charged) && ((currentLevel > 1) || (currentLevel == 0 )))
        {
            Shoot();
            Charged(false);
        }
    }

    public void SetLevel(int Level)
    {
        currentLevel = Level;
    }

    void Shoot()
    {
        ParticleSystem em = GetComponentInParent<ParticleSystem>();
        em.Play(true);

        stats.ChangePower(-100f);

    }

    public void Charged(bool Charged)
    {
        _charged = Charged;
    }

  
}
