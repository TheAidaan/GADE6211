using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    bool _showEndMenu, _showLeaderBoard;

    GameObject[] UIParts = new GameObject[2];
    
    void Awake()
    {
        _showEndMenu = true;


        int x = 0;
        for (int i = 1; i < 3; i++)
        {
            UIParts[x] = transform.GetChild(i).gameObject;
            x++;
        }
        SetGUI();
    }

    public void SetGUI()
    { 
        UIParts[0].SetActive(_showLeaderBoard); ;
        UIParts[1].SetActive(_showEndMenu);
        
    }

    public void ShowLeaderboard()
    {
        _showLeaderBoard = true;
        _showEndMenu = false;
        SetGUI();
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Leave()
    {
        _showLeaderBoard = false;
        _showEndMenu = true;
        SetGUI();
    }
}
