using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    Transform[,] Objects = new Transform[3, 10];
    Transform[] World = new Transform[2];

    int heightChangePoint=0;

    int LastStumpPoint=0;

    int randNumber;

    int spawnPoint;
    Transform Object;

    float worldHeight;


    bool worldBroken = false;
    int stopBreak;

    int currentLevel;

    int singleLane;
    public void AssignObjects()
    {
        World = Resources.LoadAll<Transform>("World");


        Transform[] enemyObjects = new Transform[6];
        Transform[] collectorsObjects = new Transform[3];
        Transform[] powerUpObjects = new Transform[6];

        enemyObjects = Resources.LoadAll<Transform>("Level1/enemyObjects");
        collectorsObjects = Resources.LoadAll<Transform>("Level1/collectorsObjects");
        powerUpObjects = Resources.LoadAll<Transform>("Level1/powerUpObjects");


        for (int i = 0; i < enemyObjects.Length; i++)
        {
            Objects[0, i] = enemyObjects[i];

        }

        for (int i = 0; i < collectorsObjects.Length; i++)
        {
            Objects[1, i] = collectorsObjects[i];

        }

        for (int i = 0; i < powerUpObjects.Length; i++)
        {
            Objects[2, i] = powerUpObjects[i];

        }


        switch (currentLevel)
        {
            case 2:Level2();
                break;
            case 3:
                break;
            default: Level1();
                break;
        }
        
    }
   public Transform PickObject()
    {
        int randObject;

        randNumber = Random.Range(0, 100);
        randObject = Random.Range(0, 4);

        if (randNumber < 40)    //40% chance
        {
            return Objects[0, randObject]; //obstacles
            
        }
        else
        {
            if ((39 < randNumber) && (randNumber < 60))//+20% chance
            {
                return Objects[1, 0]; //collector objects
            }
            else
            {
                if ((59 < randNumber) && (randNumber < 70))//+10% chance
                {
                    if (randObject == 3)
                    {
                        randObject = 2;
                    }
                    return Objects[2, randObject];//Power-Ups
                }
                else//30% chance
                {
                    if ((currentLevel == 3) && (69 < randNumber) && (randNumber < 80) && (worldBroken == false)) //10% chance
                    {
                        BreakWorld();
                    }
                    return null;
                }
            }

        }
    }

    public void SpawnBuildingBlocks(int zSpawnPoint, Transform gObject)
    {
        int randLane;
        spawnPoint = zSpawnPoint;
        Object = gObject;


        randLane = Random.Range(-1, 2);


        RaisePlatform();

        if (Object == null)
        {
            randLane = 4;
        }else
        {
            if (Object.gameObject.name == "3.Stump")
            {
                randLane = StumpCheck();
            }
                     
        }

        if(worldBroken)
        {
            if (singleLane == -1)
            {
                Instantiate(World[1], new Vector3(-2f, worldHeight, spawnPoint), World[1].rotation);            //world- left wall
            }

        }else
        {
            Instantiate(World[1], new Vector3(-2f, worldHeight, spawnPoint), World[1].rotation);            //world- left wall

        }

        for (int i = -1; i < 2; i++)
        {
            if (worldBroken)
            {
                if (i == singleLane)
                {

                    if (Object != null)
                    {
                        if (Object.gameObject.tag == "AK")             //change the tag name later
                        {
                            SpawnObject(i);
                        }

                    }
                    else
                    {
                        Instantiate(World[0], new Vector3(i, worldHeight, spawnPoint), World[0].rotation);      //world-blocks 

                    }
                }
            }else
            {
                if ((i == randLane))

                {
                    SpawnObject(i);
                    
                }else
                {
                    Instantiate(World[0], new Vector3(i, worldHeight, spawnPoint), World[0].rotation);        //world-blocks
                }
            }
        }// end for-loop


        if(worldBroken)
        {
            if (singleLane == 1)
            {
                Instantiate(World[1], new Vector3(2f, worldHeight, spawnPoint), World[1].rotation);  //world-right wall
            }

        }else
        {
            Instantiate(World[1], new Vector3(2f, worldHeight, spawnPoint), World[1].rotation);  //world-right wall
        }
        
        if (stopBreak == spawnPoint)
        {
            worldBroken = false;
        }

    }

    public virtual void SetLevel(int Level)
    {
        currentLevel = Level;
    }
    void Level1()
    {
        Objects[0, 3] = null;
    }

    void SpawnObject(int CurrentLane)
    {
        if (Object.gameObject.name == "2.Hole")
        {

            Instantiate(Object, new Vector3(CurrentLane, worldHeight, spawnPoint), Object.rotation);

        }
        else
        {
            Instantiate(World[0], new Vector3(CurrentLane, worldHeight, spawnPoint), World[0].rotation);              //world - blocks
            Instantiate(Object, new Vector3(CurrentLane, worldHeight + 1, spawnPoint), Object.rotation);
        }
    }

    #region Level 2 attributes
    void Level2()
    {
        Objects[0, 0] = null;
    }
    int StumpCheck()
    {
   
            if (LastStumpPoint < (spawnPoint - 3))
            {
                
                LastStumpPoint = spawnPoint;
                return 0;
            }
            else
            {
                return 4;
            }
        
    }

    void RaisePlatform()
    {
        if ((currentLevel > 1) && (heightChangePoint < spawnPoint - 8))
        {
            randNumber = Random.Range(0, 100);

            if (randNumber < 11)
            {
                heightChangePoint = spawnPoint;
                worldHeight += 4;
                Object = null;
                spawnRaiser();
            }
        }

        if (heightChangePoint >= spawnPoint - 2)
        {
            Object = null;
        }
    }
    void spawnRaiser()
    {
        
        int randLane = Random.Range(-1, 2);
        if (worldBroken)
        {
            randLane = singleLane;
        }
        Instantiate(Objects[2, 3], new Vector3(randLane, worldHeight-3, spawnPoint-1), Objects[2, 3].rotation);

    }

    #endregion

    #region Level 3 attributes

    void BreakWorld()
    {
        randNumber = Random.Range(-1, 2);

        singleLane = randNumber;
        stopBreak = spawnPoint+15;

        worldBroken = true;
    }

    #endregion 


}

