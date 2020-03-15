using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int coinTotal;
    public static float timeTotal;

    public static float zVelAdj;
    public static float vertVel;

    public static bool characterDeath;
    float waitToLoad = 0;

    private Spawner spawner;
    public Transform Character;
    public static Transform Player;

    // Start is called before the first frame update
    void Awake()
    {
    
        characterDeath = false;
    
        zVelAdj = 1;
        vertVel = 0;
        coinTotal = 0;
        timeTotal = 0;

        Instantiate(Character, new Vector3(0, 1, 0), Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        spawner = FindObjectOfType<Spawner>();

        spawner.playerPosZ = 8;


    }
    void Start()
    {
        
        spawner.SpawnBuildingBlocks();

    }

    // Update is called once per frame
    void Update()
    {
        if (characterDeath == false)
        {
            spawner.playerPosZ = Player.position.z;
            spawner.SpawnBuildingBlocks();
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
   
}
