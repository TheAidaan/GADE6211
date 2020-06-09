using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{

    public static int CoinsTot;
    public static float DisTot;

    void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }
    

}
