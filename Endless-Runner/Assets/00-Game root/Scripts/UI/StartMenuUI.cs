using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    readonly GameObject[] MenuParts = new GameObject[3];
    bool _showLeaderBoard, _showMainScreen;
    
    void Start()
    {
        _showLeaderBoard = false;
        _showMainScreen = true;
        int x = 0;
        for (int i = 1; i < 3; i++)
        {
            MenuParts[x] = transform.GetChild(i).gameObject;
            x++;
        }

        SetGUI();

    }

    void SetGUI()
    {
        MenuParts[0].gameObject.SetActive(_showLeaderBoard);
        MenuParts[1].gameObject.SetActive(_showMainScreen);
    }

    public void ShowLeaderboard()
    {
        _showLeaderBoard = true;
        _showMainScreen = false;
        SetGUI();
    } 

    public void Leave()
    {
        _showLeaderBoard = false;
        _showMainScreen = true;
        SetGUI();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
