using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI[] DisplayText = new TextMeshProUGUI[2];
    Slider characterPowerIndicator;
    GameObject[] GameParts = new GameObject[3];

    bool _showPowerIndicator, _showDistanceText, _showCoinsText;
    bool _showLeaderBoard, _showGameOver, _showSaveScore;

    int _score;
    public void Begin()
    {
        _showDistanceText = true;
        _showCoinsText = true;
        _showPowerIndicator = false; 
        _showLeaderBoard = false; 
        _showGameOver = false; 
        _showSaveScore = false;
 

        characterPowerIndicator = GetComponentInChildren< UnityEngine.UI.Slider >();
        DisplayText = GetComponentsInChildren<TextMeshProUGUI>();
        int x = 0;
        for (int i = 3; i<6;i++)
        {
            GameParts[x] = transform.GetChild(i).gameObject;
            x++;
        }

        SetGUI();
    }

    public void SetGUI()
    {
        characterPowerIndicator.gameObject.SetActive(_showPowerIndicator);
        DisplayText[0].gameObject.SetActive(_showCoinsText);
        DisplayText[1].gameObject.SetActive(_showDistanceText);

        GameParts[0].gameObject.SetActive(_showLeaderBoard); ; 
        GameParts[1].gameObject.SetActive(_showGameOver);
        GameParts[2].gameObject.SetActive(_showSaveScore);
    }
    public void SetSlider(float value)
    {
        characterPowerIndicator.value = value;
    }
   
    public void SetTotCoins(string coinTot)
    {
        DisplayText[0].text = "Coins: " + coinTot;
    }

    public void SetTotDist(int disTot)
    {
        DisplayText[1].text = disTot.ToString() + "M";
        _score = disTot;
    }

    public void PlayerDeath()
    {
        _showPowerIndicator =false;
        _showCoinsText = false;

        _showSaveScore = true;
        DisplayText[1].text = "Score: " + _score.ToString();
        DisplayText[1].GetComponent<RectTransform>().localPosition = new Vector3(-40, 300, 0);
        _showDistanceText = true;

        SetGUI();
    }

    public void ShowGameOver()
    {
        _showSaveScore = false;
        _showDistanceText = false;
        _showGameOver = true;

        SetGUI();

    }

    public void ShowLeaderboard()
    {
        _showLeaderBoard = true;
        _showGameOver = false;
        SetGUI();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowPowerIndicator(bool showPowerIndicator)
    {
        _showPowerIndicator = showPowerIndicator;
        SetGUI();
    }
    public void ShowDistanceText(bool showDistanceText)
    {
        _showDistanceText = showDistanceText;
        SetGUI();
    }

    public void SaveScore(TMP_InputField name)
    {
        Data.AddNewHighscore(name.text, _score);
    }

    public void ChangeColour(Material playerMaterial)
    {
        DisplayText[1].color = playerMaterial.color;

    }
}
