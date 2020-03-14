using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stats : MonoBehaviour
{

    Text aidan;

    // Start is called before the first frame update
    void Start()
    {
        aidan = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.name == "CoinCounter.txt")
        {
            GetComponent<TextMesh>().text = "Coins collected: " + GameManager.coinTotal;      //prints the total coins at the end of the lvl
        } 
        if (gameObject.name == "time.txt")
        { 
            GetComponent<TextMesh>().text = "Time taken: " + GameManager.timeTotal;            //prints the total time taken to complete lvl
        }
        
        //if (gameObject.name == "aidan.txt")
        //{
        //    GetComponent<TextMesh>().text = "aidan: " + GameManager.timeTotal;

        //}

        aidan.text = "aidan: " + GameManager.coinTotal;


    }
}
