using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject Character;

    CharacterStats Stats;

    void Awake()
    { 
        Stats = Character.GetComponent<CharacterStats>();
    }

    void Update()
    {
        score.text = "Coins: " + Stats.Coins();

    }
}
