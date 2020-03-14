using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{

    [SerializeField] private Text score;

    void Update()
    {
        score.text = "Score: " + GameManager.coinTotal;

    }
}
