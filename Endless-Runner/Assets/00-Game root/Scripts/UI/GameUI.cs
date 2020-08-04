using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    int _score;

    bool  _showDistanceText;
    bool _showGameOver, _showPause;

    GameObject[] UIParts = new GameObject[2];
    TextMeshProUGUI _distanceText;
    Slider characterPowerIndicator;

    void Awake()
    {
        _showDistanceText = true;

        int x = 0;
        for (int i = 3; i < 5; i++)
        {
            UIParts[x] = transform.GetChild(i).gameObject;
            x++;
        }


        characterPowerIndicator = GetComponentInChildren<UnityEngine.UI.Slider>();
        _distanceText = GetComponentInChildren<TextMeshProUGUI>();
        Continue();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_showPause)
            {
                Pause();
            }else
            {
                Continue();
            }
            
        }
    }
    public void SetGUI()
    {
        characterPowerIndicator.gameObject.SetActive(GameManager.CharacterAbility);
        _distanceText.gameObject.SetActive(_showDistanceText);

        UIParts[0].SetActive(_showGameOver); ;
        UIParts[1].SetActive(_showPause);

    }

    public void SetSlider(float value)
    {
        characterPowerIndicator.value = value;
    }

 
    public void SetTotDist(int disTot)
    {
        _distanceText.text = disTot.ToString() + "M";
        _score = disTot;
    }

    public void PlayerDeath()
    {

        _showGameOver = true;
        _distanceText.text = "Score: " + _score.ToString();
        _distanceText.GetComponent<RectTransform>().localPosition = new Vector3(-40, 300, 0);
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
        _distanceText.color = playerMaterial.color;
    }

    public void SaveScore(TMP_InputField name)
    {
        Data.AddNewHighscore(name.text, _score);
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
