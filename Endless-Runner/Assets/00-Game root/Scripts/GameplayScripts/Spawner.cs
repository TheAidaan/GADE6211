using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Transform[,] Objects = new Transform[3, 10];
    Transform[] World = new Transform[4];
    Transform[] Triggers = new Transform[2];
    Transform[,] BossObjects = new Transform[3,10];

    int randNumber;
    int _firstLane;

    int spawnPoint;
    Transform Object;

    float worldHeight;
    int heightChangePoint = 0;
    int LastStumpPoint = 0;


    bool worldBroken = false;
    int singleLane;
    int stopBreak;
    int _singleObjectsSwpawned = 0;

    bool _raiseWorld;

    int ClearDistance;
    bool _pathClear;

    GameObject _parent;
    public void AssignObjects()
    {
        World = Resources.LoadAll<Transform>("Prefabs/World");
        Triggers = Resources.LoadAll<Transform>("Prefabs/Triggers");


        Transform[] enemyObjects = new Transform[4];
        Transform[] collectorsObjects = new Transform[2];
        Transform[] powerUpObjects = new Transform[3];

        enemyObjects = Resources.LoadAll<Transform>("Prefabs/enemyObjects");
        collectorsObjects = Resources.LoadAll<Transform>("Prefabs/collectorsObjects");
        powerUpObjects = Resources.LoadAll<Transform>("Prefabs/powerUpObjects");

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

        GetLevelAttributes();
    }
   public Transform PickObject()
    {
        int randObject;

        randNumber = Random.Range(0, 100);
        

        if (randNumber < 25)    //20% chance
        {
            randObject = Random.Range(0, 4);
            return Objects[0, randObject]; //obstacles
            
        }
        else
        {
            if ((24 < randNumber) && (randNumber < 35))//+-10% chance
            {
                randObject = Random.Range(0, 2);
                return Objects[1, randObject]; //collector objects
            }
            else
            {
                if ((59 < randNumber) && (randNumber < 70))//+10% chance
                {
                    randObject = Random.Range(0, 3);
                    return Objects[2, randObject];//Power-Ups
                }
                else//30% chance
                {
                    if ((GameManager.CurrentLevel == 3) && (95 < randNumber) && (randNumber < 101) && (worldBroken == false)) //5% chance
                    {
                        BreakWorld();
                    }

                    if ((GameManager.CurrentLevel > 1) && (90 < randNumber) && (randNumber < 96) && (_raiseWorld == false)) //5% chance
                    {
                        _raiseWorld = true;
                        ClearPath(2);
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


        randLane = Random.Range(_firstLane, _firstLane + 3);

        if (!_pathClear)
        {
            Object = null;
            if (ClearDistance == spawnPoint)
            {
                _pathClear = true;
            }
        }

        if (_raiseWorld &&_pathClear)
        {
           RaisePlatform();
           _raiseWorld = false;        
        }


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
            if (singleLane == _firstLane)
            {
                Instantiate(World[1], new Vector3(_firstLane-1, worldHeight, spawnPoint), World[1].rotation, _parent.transform);            //world- left wall
            }

        }else
        {
                Instantiate(World[1], new Vector3(_firstLane-1, worldHeight, spawnPoint), World[1].rotation,_parent.transform);       //world- left wall

        }

        for (int i = _firstLane; i < _firstLane + 3; i++)
        {
            if (worldBroken)
            {
                if (i == singleLane)
                {

                    if (Object != null)
                    {
                        if ((Object.gameObject.tag == "Single"))             //change the tag name later
                        {
                            SpawnObject(i, gObject, true);
                            _singleObjectsSwpawned++;

                            if(_singleObjectsSwpawned == 2)
                            {
                                ClearPath(2);
                                _singleObjectsSwpawned = 0;
                            }
                        }else
                        {
                            Instantiate(World[0], new Vector3(i, worldHeight, spawnPoint), World[0].rotation, _parent.transform);      //world- ground blocks 
                        }

                    }
                    else
                    {
                        Instantiate(World[0], new Vector3(i, worldHeight, spawnPoint), World[0].rotation, _parent.transform);      //world- ground blocks 

                    }
                }else
                {
                    Instantiate(Objects[0,2], new Vector3(i, worldHeight, spawnPoint), Objects[0, 2].rotation, _parent.transform);
                }
            }else
            {
                if ((i == randLane))
                {
                    SpawnObject(i, gObject,true);
                    
                }else
                {
                    Instantiate(World[0], new Vector3(i, worldHeight, spawnPoint), World[0].rotation, _parent.transform);        //world- ground blocks
                }
            }
        }// end for-loop


        if(worldBroken)
        {
            if (singleLane == 1)
            {
                Instantiate(World[1], new Vector3(_firstLane+3, worldHeight, spawnPoint), World[1].rotation, _parent.transform);  //world-right wall
            }

        }else
        {
            Instantiate(World[1], new Vector3(_firstLane+3, worldHeight, spawnPoint), World[1].rotation, _parent.transform);  //world-right wall
        }
        
        if (stopBreak == spawnPoint)
        {
            worldBroken = false;
            ClearPath(3);
        }

    }


    public void SpawnObject(float currentLane, Transform gObject, bool spawnground)
    {
        float offset = (Mathf.Abs(currentLane) % 1);

        if (offset > 0)
        {
            offset--;
        }

        if (gObject != null)
        {
            if (gObject.gameObject.name == "2.Hole")
            {
                Instantiate(gObject, new Vector3(currentLane, worldHeight, spawnPoint), gObject.rotation, _parent.transform);
                spawnground = false;
            }
            else
            {
                if (gObject.gameObject.name == "3.Stump")
                {
                    currentLane = (_firstLane + 1) + Mathf.Abs(offset);
                }                             
                Instantiate(gObject, new Vector3(currentLane, worldHeight + 1, spawnPoint), gObject.rotation, _parent.transform);
            }
        }

        if (spawnground)
        {
            Instantiate(World[0], new Vector3(currentLane, worldHeight, spawnPoint), World[0].rotation, _parent.transform); //world - ground blocks
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
                return _firstLane + 1;
            }
            else
            {
                return _firstLane + 5;
            }
        
    }

    void RaisePlatform()
    {
       
            randNumber = Random.Range(0, 100);

        if (randNumber < 11)
        {
            heightChangePoint = spawnPoint;
                worldHeight += 4;
                Object = null;
                spawnRaiser();
        }


        if (heightChangePoint >= spawnPoint - 2)
        {
            Object = null;
        }
    }
    void spawnRaiser()
    {
        
        int randLane = Random.Range(_firstLane, _firstLane + 3);
        if (worldBroken)
        {
            randLane = singleLane;
        }
        Instantiate(World[2], new Vector3(randLane, worldHeight-3, spawnPoint-1), World[2].rotation, _parent.transform);
        Instantiate(Triggers[2], new Vector3(_firstLane+1, 1.5f, spawnPoint+1), Triggers[2].rotation, _parent.transform); // the gone trigger
        
        ClearPath(3);

    }

    #endregion

    #region Level 3 attributes

    void BreakWorld()
    {
        int randNumberLength = Random.Range(15, 30);
        randNumber = Random.Range(_firstLane, _firstLane + 3);

        singleLane = randNumber;
        stopBreak = spawnPoint+ randNumberLength;

        worldBroken = true;
    }

    #endregion

    #region Level variations
    void GetLevelAttributes()
    {
        if (GameManager.BossMode)
        {
            switch (GameManager.CurrentLevel)
            {
                case 1:
                    Objects[0, 1] = null; //removes moving object
                    Objects[0, 2] = null; //removes hole object

                    Objects[2, 2] = null; //removes Fling powerup

                    Objects[1, 1] = null; //removes Fling powerup
                    break;
                case 2:
                    Objects[0, 0] = null;//removes cube object 
                    Objects[2, 0] = null;//removes immunity powerUp
                    break;
                case 3:
                    Objects[2, 1] = null;//removes superSize powerUp
                    break;
                default:
                    break;

            }

        }
        else
        {
            switch (GameManager.CurrentLevel)
            {
                case 1:
                    Objects[0, 3] = null; //removes stump object
                    Objects[1, 1] = null; //removes powercharge
                    break;
                case 2:
                    Objects[0, 0] = null;//removes cube object 
                    Objects[2, 0] = null;//removes immunity powerUp
                    break;
                case 3:
                    Objects[2, 1] = null;//removes superSize powerUp
                    break;
                default:
                    break;

            }
        }

    }
    #endregion

    void ClearPath(int Length)
    {
        ClearDistance = spawnPoint + 2;
        _pathClear = false;
    }

    public Transform GetSpecificObject(int Category,int Item)
    {
        return Objects[Category, Item];
    }
    public Transform GetWorldBlocks(int index)
    {
        return World[index];
    }

    public void SpawnPlatform(int spawnPoint, Transform superFling, float Rotation)
    {
        Instantiate(World[3], new Vector3(0f, worldHeight, spawnPoint + 2), Quaternion.Euler(0,Rotation,0));

        if (superFling != null)
        {
            Instantiate(Triggers[1], new Vector3(0f, worldHeight + 1f, spawnPoint + 1), Triggers[1].rotation);// spawn the center player trigger
            Instantiate(superFling, new Vector3(_firstLane + 1, worldHeight + 1f, spawnPoint + 2), superFling.rotation, _parent.transform);
        }
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    public void SetLanes(int firstLane)
    {
        _firstLane = firstLane;
    }

    public void SetSpawnPoint(int zSpawnPoint)
    {
        spawnPoint = zSpawnPoint;
    }

    public void SpawnEscape(int spawnPoint, Transform parent)
    {
        for (int z = spawnPoint; z < (spawnPoint + 3); z++)
        {
            for (int x = _firstLane; x < _firstLane+3; x++)
            {
                Instantiate(World[0], new Vector3(x, 0, z), World[0].rotation, parent);
            }

            Instantiate(World[1], new Vector3(_firstLane+3, 0, z), World[1].rotation, parent);
        }
    }



}

