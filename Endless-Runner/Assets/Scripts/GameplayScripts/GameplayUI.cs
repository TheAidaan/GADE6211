using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;

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
