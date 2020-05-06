using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterUI : MonoBehaviour
{

     TextMeshProUGUI score;
     Slider PowerIndicator;
    private void Awake()
    {
        PowerIndicator =  GetComponentInChildren<UnityEngine.UI.Slider>();
        score = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetGUI()
    {
        if (GameManager.CurrentLevel == 1)
        {
            PowerIndicator.gameObject.SetActive(false); ;
        }
        else
        {
            PowerIndicator.gameObject.SetActive(true); ;
        }

    }

    public void SetSlider(float value)
    {
        PowerIndicator.value = value;
    }

    public void SetTotCoins(string coinTot)
    {
        score.text = "Coins: " + coinTot;
    }
       
    





}

