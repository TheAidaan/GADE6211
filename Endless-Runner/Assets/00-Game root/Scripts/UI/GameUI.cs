using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    int _score;

    bool _showPowerIndicator, _showDistanceText, _showCoinsText;
    bool _showGameOver, _showPause;

    GameObject[] UIParts = new GameObject[2];
    TextMeshProUGUI[] DisplayText = new TextMeshProUGUI[2];
    Slider characterPowerIndicator;

    void Awake()
    {
        _showDistanceText = true;
        _showCoinsText = true;

        int x = 0;
        for (int i = 4; i < 6; i++)
        {
            UIParts[x] = transform.GetChild(i).gameObject;
            x++;
        }


        characterPowerIndicator = GetComponentInChildren<UnityEngine.UI.Slider>();
        DisplayText = GetComponentsInChildren<TextMeshProUGUI>();
        SetGUI();

    }
    public void SetGUI()
    {
        characterPowerIndicator.gameObject.SetActive(_showPowerIndicator);
        DisplayText[0].gameObject.SetActive(_showCoinsText);
        DisplayText[1].gameObject.SetActive(_showDistanceText);

        UIParts[0].SetActive(_showGameOver); ;
        UIParts[1].SetActive(_showPause);

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
        _showPowerIndicator = false;
        _showCoinsText = false;

        _showGameOver = true;
        DisplayText[1].text = "Score: " + _score.ToString();
        DisplayText[1].GetComponent<RectTransform>().localPosition = new Vector3(-40, 300, 0);
        _showDistanceText = true;

        SetGUI();
    }

    public void ShowDistance(bool showDistance)
    {
        _showDistanceText = showDistance;
        SetGUI();
    }

    public void ChangeColour(Material playerMaterial)
    {
        DisplayText[1].color = playerMaterial.color;
    }

    public void SaveScore(TMP_InputField name)
    {
        Data.AddNewHighscore(name.text, _score);
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
    public void Continue()
    {
        Time.timeScale = 1;
        _showPause = false;
        SetGUI();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        _showPause = true;
        SetGUI();
    }

    public void Leave()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
