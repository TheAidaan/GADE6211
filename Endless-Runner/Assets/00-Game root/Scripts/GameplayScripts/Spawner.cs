using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    readonly Transform[,] Objects = new Transform[3, 10];
    Transform[] World = new Transform[5];
    Transform[] Triggers = new Transform[2];
    Transform Object;
    Transform _parent;

    static int _worldHeight;
    public static int WorldHeight { get { return _worldHeight; } }

    static float _firstLane;
    public static float FirstLane { get { return _firstLane; } }

    bool _worldBroken = false;
    bool _raiseWorld;
    bool _pathClear = true;

    int _stopBreak, _clearDistance, _spawnPoint;
    int _singleObjectsSwpawned = 0;
    int _heightChangePoint = 0;
    int _lastStumpPoint = 0;


    float _singleLane;
    
    public void AssignObjects()
    {
        World = Resources.LoadAll<Transform>("Prefabs/World");
        Triggers = Resources.LoadAll<Transform>("Prefabs/Triggers");

        Transform[] enemyObjects = Resources.LoadAll<Transform>("Prefabs/enemyObjects");
        Transform[] collectorsObjects = Resources.LoadAll<Transform>("Prefabs/collectorsObjects");
        Transform[] powerUpObjects = Resources.LoadAll<Transform>("Prefabs/powerUpObjects");

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

        int randNumber = RandNum();


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
                    if ((GameManager.CurrentLevel == 3) && (95 < randNumber) && (randNumber < 101) && (_worldBroken == false)) //5% chance
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

    float RandLane()
    {
        return _firstLane + Random.Range(1, 4);
    }
    int RandNum()
    {
        return Random.Range(0, 100);
    }

    public void SpawnBuildingBlocks(int SpawnPoint, Transform gObject)
    {
        float randLane = RandLane();
        _spawnPoint = SpawnPoint;
        Object = gObject;

        if (!_pathClear)
        {
            Object = null;
            if (_clearDistance == _spawnPoint)
            {
                _pathClear = true;
            }
        }

        if (_raiseWorld && _pathClear)
        {
            RaisePlatform();
            _raiseWorld = false;
        }


        if (Object == null)
        {
            randLane = 4;
        }
        else
        {
            if (Object.gameObject.name == "3.Stump")
            {
                randLane = StumpCheck();
            }
        }

        if (_worldBroken)
        {
            if (_singleLane == _firstLane)
            {
                Instantiate(World[1], new Vector3(_firstLane - 1, _worldHeight, _spawnPoint), World[1].rotation, _parent);            //world- left wall
            }

        }
        else
        {
            Instantiate(World[1], new Vector3(_firstLane - 1,  _worldHeight, _spawnPoint), World[1].rotation, _parent);       //world- left wall

        }

        for (float x = _firstLane; x < _firstLane + 3; x++)
        {
            if (_worldBroken)
            {
                if (x == _singleLane)
                {

                    if (Object != null)
                    {
                        if (Object.CompareTag("Single"))             //change the tag name later
                        {
                            SpawnObject(x, gObject, true);
                            _singleObjectsSwpawned++;

                            if (_singleObjectsSwpawned == 2)
                            {
                                ClearPath(2);
                                _singleObjectsSwpawned = 0;
                            }
                        }
                        else
                        {
                            Instantiate(World[0], new Vector3(x, _worldHeight, _spawnPoint), World[0].rotation, _parent);      //world- ground blocks 
                        }

                    }
                    else
                    {
                        Instantiate(World[0], new Vector3(x, _worldHeight, _spawnPoint), World[0].rotation, _parent);      //world- ground blocks 

                    }
                }
                else
                {
                    Instantiate(Objects[0, 2], new Vector3(x, _worldHeight, _spawnPoint), Objects[0, 2].rotation, _parent);
                }
            }
            else
            {
                if ((x == randLane))
                {
                    SpawnObject(x, gObject, true);

                }
                else
                {
                    Instantiate(World[0], new Vector3(x, _worldHeight, _spawnPoint), World[0].rotation, _parent);        //world- ground blocks
                }
            }
        }// end for-loop


        if (_worldBroken)
        {
            if (_singleLane == 1)
            {
                Instantiate(World[1], new Vector3(_firstLane + 3, _worldHeight, _spawnPoint), World[1].rotation, _parent);  //world-right wall
            }

        }
        else
        {
            Instantiate(World[1], new Vector3(_firstLane + 3, _worldHeight, _spawnPoint), World[1].rotation, _parent);  //world-right wall
        }

        if (_stopBreak == _spawnPoint)
        {
            _worldBroken = false;
            ClearPath(3);
        }

    }


    public void SpawnObject(float currentLane, Transform gObject, bool spawnground)
    {


        if (gObject != null)
        {
            if (gObject.gameObject.name == "2.Hole")
            {
                Instantiate(gObject, new Vector3(currentLane, _worldHeight, _spawnPoint), gObject.rotation);
                spawnground = false;
            }
            else
            {
                if (gObject.gameObject.name == "3.Stump")
                {
                    currentLane = (_firstLane + 1);
                }
                Instantiate(gObject, new Vector3(currentLane, _worldHeight + 1, _spawnPoint), gObject.rotation);
            }
        }

        if (spawnground)
        {
            Instantiate(World[0], new Vector3(currentLane, _worldHeight, _spawnPoint), World[0].rotation, _parent); //world - ground blocks
        }


    }

    #region Level 2 attributes
  
    float StumpCheck()
    {
        if (_lastStumpPoint < (_spawnPoint - 3))
        {

            _lastStumpPoint = _spawnPoint;
            return _firstLane + 1;
        }
        else
        {
            return -15;
        }

    }

    void RaisePlatform()
    {
        if (RandNum() < 11)
        {
            _heightChangePoint = _spawnPoint;
            _worldHeight += 4;
            Object = null;
            SpawnRaiser();
        }


        if (_heightChangePoint >= _spawnPoint - 2)
        {
            Object = null;
        }
    }
    void SpawnRaiser()
    {

        float randLane = RandLane();
        if (_worldBroken)
        {
            randLane = _singleLane;
        }
        Instantiate(World[2], new Vector3(randLane, _worldHeight - 3, _spawnPoint - 1), World[2].rotation, _parent);
        Instantiate(Triggers[2], new Vector3(_firstLane + 1, 1.5f, _spawnPoint + 1), Triggers[2].rotation, _parent); // the gone trigger

       ClearPath(3);

    }

    #endregion

    #region Level 3 attributes

    void BreakWorld()
    {
        int breakLength = Random.Range(15, 30);

        _singleLane = RandLane();
        _stopBreak = _spawnPoint + breakLength;

        _worldBroken = true;
    }

    public void ForceBreak(int lane, int breakLength)
    {
        _singleLane = _firstLane + (lane-1);
        _stopBreak = _spawnPoint + breakLength;
        _worldBroken = true;
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

        _clearDistance = _spawnPoint + Length;

        _pathClear = false;
    }

    public void SpawnPlatform(int spawnPoint, bool going)
    {
        if (going)
        {
            Transform platform = Instantiate(World[3], new Vector3(0f, _worldHeight, spawnPoint + 2), World[3].rotation);
            Instantiate(World[4], new Vector3(_firstLane + 1, _worldHeight+ 0.5001f, spawnPoint + 2), World[4].rotation, platform);
        }else
        {
            Instantiate(World[3], new Vector3(_firstLane + 1, _worldHeight, spawnPoint + 2), Quaternion.Euler(0, 180, 0));
        }
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent.transform;
    }

    public void SetLanes(float firstLane)
    {
        _firstLane = firstLane;
    }

    public void SetSpawnPoint(int SpawnPoint)
    {
        _spawnPoint = SpawnPoint;
    }
    public void SetWorldHeight(int worldHeight)
    {
        _worldHeight = worldHeight;
    }

}