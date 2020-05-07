using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GUI : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI coinsTxt,distTxt;

    public static int CoinsTot;
    public static float DisTot;

    CharacterStats Stats;

    void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }
    private void Awake()
    {
    }
    void Start()
    {
        coinsTxt.text = "Coins: " + CoinsTot.ToString();
        distTxt.text = "Distance: " + DisTot.ToString();
    }

    public void setStats(int coinsTot, float disTot)
    {
        CoinsTot = coinsTot;
        DisTot = disTot;
    }


}
