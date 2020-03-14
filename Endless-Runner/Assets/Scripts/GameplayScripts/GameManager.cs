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

    private Vector3 playerStartPoint;

    private Spawner spawner;
    private SelfDestruct selfDestruct;


    public Transform Character;
    public static Transform Player;

    // Start is called before the first frame update
    void Awake()
    {
    
        characterDeath = false;
        playerStartPoint = new Vector3(0, 1, 0);
        zVelAdj = 1;
        vertVel = 0;
        coinTotal = 0;
        timeTotal = 0;

        Instantiate(Character, playerStartPoint, Character.rotation);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        selfDestruct = FindObjectOfType<SelfDestruct>();
        spawner = FindObjectOfType<Spawner>();

        spawner.playerPosZ = playerStartPoint.z;


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
            //selfDestruct.playerPosZ = Player.position.z;
            timeTotal += Time.deltaTime;
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
