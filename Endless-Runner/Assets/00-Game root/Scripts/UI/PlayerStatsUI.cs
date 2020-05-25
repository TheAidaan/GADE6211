using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    /*[SerializeField]*/ TextMeshProUGUI distanceText;
    /*[SerializeField]*/ TMP_InputField inputField;
    public static int  TotalDistance;

    string username;

    void Start()
    {
        distanceText = GetComponentInChildren<TextMeshProUGUI>();
        inputField = GetComponentInChildren<TMP_InputField>();

        distanceText.text = "Distance: " + TotalDistance;
    }

    public void SaveScore()
    {
        username = inputField.text;
        Data.AddNewHighscore(username, TotalDistance);
    }
}
