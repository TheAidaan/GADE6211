using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterUI : MonoBehaviour
{

    TextMeshProUGUI[] DisplayText = new TextMeshProUGUI[2];
     Slider PowerIndicator;
    private void Awake()
    {
        PowerIndicator =  GetComponentInChildren<UnityEngine.UI.Slider>();
        DisplayText = GetComponentsInChildren<TextMeshProUGUI>();
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
        DisplayText[0].text = "Coins: " + coinTot;
    }

    public void SetTotDist(string disTot)
    {
        DisplayText[1].text = "Distance: " + disTot;
    }







}

