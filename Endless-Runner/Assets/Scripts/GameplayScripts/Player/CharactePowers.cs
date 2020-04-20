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

    public void SetLevel()
    {
        currentLevel = FindObjectOfType<GameManager>().CurrentLevel();
    }

    void Shoot()
    {

    }
}
