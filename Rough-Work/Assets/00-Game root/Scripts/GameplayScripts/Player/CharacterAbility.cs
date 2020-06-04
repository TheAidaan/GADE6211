using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    CharacterStats stats;
    CharacterReact React;
    ParticleSystem em;

    bool _charged = false;
    void Start()
    {
        stats = GetComponentInChildren<CharacterStats>();
        React = GetComponentInChildren<CharacterReact>();
        em = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.S)) && (_charged) && ((GameManager.CurrentLevel > 1) || (GameManager.CurrentLevel == 0 )))
        {
            Shoot();
            stats.ChangePower(-100f);
            Charged(false);
        }
    }

    public void Shoot()
    {
        em.GetComponent<ParticleSystemRenderer>().material = React.CurrentMaterial();
        em.Play(true);
    }

    public void Charged(bool Charged)
    {
        _charged = Charged;
    }

  
}
