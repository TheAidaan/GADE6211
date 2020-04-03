using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int coinTotal;
    public static float timeTotal;

    public static float zVelAdj;

    public static bool characterDeath;
    float waitToLoad = 0;

    Spawn spawn;
    public Transform Character;
    public static Transform Player;

    Transform[,] Objects = new Transform[3, 3];
    

    int spawnPoint = 12;

    // Start is called before the first frame update
    void Awake()
    {
    
        characterDeath = false;

        zVelAdj = 1;
        coinTotal = 0;
        timeTotal = 0;

        Instantiate(Character, new Vector3(0, 1, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        spawn = FindObjectOfType<Spawn>();
        spawn.AssignObjects();

        


    }
    void Start()
    {

        for (int spawnPoint = 0; spawnPoint < 12; spawnPoint++)
        {
            spawn.SpawnBuildingBlocks(spawnPoint,null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (characterDeath == false)
        {
            if (Player.position.z>(spawnPoint-15))
             {
                spawn.SpawnBuildingBlocks(spawnPoint, spawn.spawnObject());
                spawnPoint++;
            }

            timeTotal += Time.deltaTime;
            
            if (timeTotal >10)
            {
                zVelAdj = timeTotal / 10;
            }
            
        }


        if (characterDeath == true)
        {
            waitToLoad += Time.deltaTime;
        }

        if (waitToLoad > 2)
        {
            SceneManager.LoadScene("GameOverMenu");
        }

    }



}//Gamemanager
