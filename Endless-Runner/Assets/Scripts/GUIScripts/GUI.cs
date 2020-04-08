using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GUI : MonoBehaviour
{
   [SerializeField] Text coinsTot,timetot;

    CharacterStats Stats;

    void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    void Start()
    {
        Stats = FindObjectOfType<CharacterStats>();

        if ((coinsTot!=null)&&(timetot!=null))
        {
            coinsTot.text = "coinsTot:  " + Stats.Coins();
            timetot.text = "coinsTot:  " + Stats.Coins();
        }
        
    }


}
