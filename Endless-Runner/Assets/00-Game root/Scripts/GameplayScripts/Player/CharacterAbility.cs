using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    CharacterStats stats;
    CharacterReact React;
    ParticleSystem em;
    ParticleSystem.MainModule emSettings;

    bool _charged = false;
    void Start()
    {
        stats = GetComponentInChildren<CharacterStats>();
        React = GetComponentInChildren<CharacterReact>();
        em = GetComponentInChildren<ParticleSystem>();
        emSettings = GetComponentInChildren<ParticleSystem>().main;
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.S)) && (_charged) && (GameManager.CharacterAbility))
        {
            Shoot();
            stats.ChangePower(-100f);
            Charged(false);
        }
    }

    public void Shoot()
    {
        emSettings.startColor = React.CurrentMaterial().color;
        em.Play(true);

        if (em.isPlaying)
        {
            Kill();
        }
       
    }

    public void Charged(bool Charged)
    {
        _charged = Charged;
    }

    void Kill()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer == 11)
            {
                Destroy(col.gameObject);
            }

        }
    }


}
