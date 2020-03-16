using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GUI : MonoBehaviour
{
   [SerializeField] Text coinsTot,timetot;

    void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    void Start()
    {
        if ((coinsTot!=null)&&(timetot!=null))
        {
            coinsTot.text = "coinsTot:  " + GameManager.coinTotal.ToString();
            timetot.text = "coinsTot:  " + GameManager.timeTotal.ToString();
        }
        
    }


}
