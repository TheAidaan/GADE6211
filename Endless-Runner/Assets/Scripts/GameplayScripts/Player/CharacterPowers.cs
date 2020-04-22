using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPowers : MonoBehaviour
{
    int currentLevel;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Q)) && (currentLevel > 1))
        {
            Shoot();
        }
    }

    public void SetLevel(int Level)
    {
        currentLevel = Level;
    }

    void Shoot()
    {
        ParticleSystem em = GetComponent<ParticleSystem>();
        em.Play(true);

    }
}
