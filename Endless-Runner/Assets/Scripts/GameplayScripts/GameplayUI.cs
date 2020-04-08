using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{

    [SerializeField] private Text score;

    CharacterStats Stats;

    void Awake()
    {
        Stats = FindObjectOfType<CharacterStats>();
    }

    void Update()
    {
        score.text = "Coins: " + Stats.Coins();

    }
}
