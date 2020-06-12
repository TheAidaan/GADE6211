using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    GameObject[] MenuParts = new GameObject[3];
    bool _showLeaderBoard, _showMainScreen;
    // Start is called before the first frame update
    void Start()
    {
        _showLeaderBoard = false;
        _showMainScreen = true;
        int x = 0;
        for (int i = 0; i < 2; i++)
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
}
