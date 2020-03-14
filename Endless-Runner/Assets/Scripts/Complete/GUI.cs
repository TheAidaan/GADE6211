using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{

    [SerializeField] private Text CoinTot, timeTot;

   
    void Start()
    {

        CoinTot.text = "Coins collected: " + GameManager.coinTotal.ToString();      
        timeTot.text = "Time taken: " + GameManager.timeTotal.ToString();            

    }

   
}
