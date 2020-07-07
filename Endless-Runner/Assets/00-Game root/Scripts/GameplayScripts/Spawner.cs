using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    readonly Transform[,] Objects = new Transform[3, 10];
    Transform[] World = new Transform[5];
    Transform[] Triggers = new Transform[2];

    int randNumber, _firstLane, spawnPoint, _worldHeight;
    Transform Object;

    float _offset;
    int heightChangePoint = 0;
    int LastStumpPoint = 0;


    bool _worldBroken = false;
    int singleLane, stopBreak;
    int _singleObjectsSwpawned = 0;

    bool _raiseWorld;

    bool _pathClear = true;

    int ClearDistance;

    Transform _parent;
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
            if (singleLane == _firstLane)
            {
                Instantiate(World[1], new Vector3(_firstLane - 1 + _offset, _worldHeight, spawnPoint), World[1].rotation, _parent);            //world- left wall
            }

        }
        else
        {
            Instantiate(World[1], new Vector3(_firstLane - 1 + _offset, _worldHeight, spawnPoint), World[1].rotation, _parent);       //world- left wall

        }

        for (int i = _firstLane; i < _firstLane + 3; i++)
        {
            if (_worldBroken)
            {
                if (i == singleLane)
                {

                    if (Object != null)
                    {
                        if (Object.CompareTag("Single"))             //change the tag name later
                        {
                            SpawnObject(i, gObject, true);
                            _singleObjectsSwpawned++;

                            if (_singleObjectsSwpawned == 2)
                            {
                                ClearPath(2);
                                _singleObjectsSwpawned = 0;
                            }
                        }
                        else
                        {
                            Instantiate(World[0], new Vector3(i + _offset, _worldHeight, spawnPoint), World[0].rotation, _parent);      //world- ground blocks 
                        }

                    }
                    else
                    {
                        Instantiate(World[0], new Vector3(i + _offset, _worldHeight, spawnPoint), World[0].rotation, _parent);      //world- ground blocks 

                    }
                }
                else
                {
                    Instantiate(Objects[0, 2], new Vector3(i + _offset, _worldHeight, spawnPoint), Objects[0, 2].rotation, _parent);
                }
            }
            else
            {
                if ((i == randLane))
                {
                    SpawnObject(i, gObject, true);

                }
                else
                {
                    Instantiate(World[0], new Vector3(i + _offset, _worldHeight, spawnPoint), World[0].rotation, _parent);        //world- ground blocks
                }
            }
        }// end for-loop


        if (_worldBroken)
        {
            if (singleLane == 1)
            {
                Instantiate(World[1], new Vector3(_firstLane + 3 + _offset, _worldHeight, spawnPoint), World[1].rotation, _parent);  //world-right wall
            }

        }
        else
        {
            Instantiate(World[1], new Vector3(_firstLane + 3 + _offset, _worldHeight, spawnPoint), World[1].rotation, _parent);  //world-right wall
        }

        if (stopBreak == spawnPoint)
        {
            _worldBroken = false;
            ClearPath(3);
        }

    }


    public void SpawnObject(int currentLane, Transform gObject, bool spawnground)
    {


        if (gObject != null)
        {
            if (gObject.gameObject.name == "2.Hole")
            {
                Instantiate(gObject, new Vector3(currentLane + _offset, _worldHeight, spawnPoint), gObject.rotation);
                spawnground = false;
            }
            else
            {
                if (gObject.gameObject.name == "3.Stump")
                {
                    currentLane = (_firstLane + 1);
                }
                Instantiate(gObject, new Vector3(currentLane + _offset, _worldHeight + 1, spawnPoint), gObject.rotation);
            }
        }

        if (spawnground)
        {
            Instantiate(World[0], new Vector3(currentLane + _offset, _worldHeight, spawnPoint), World[0].rotation, _parent); //world - ground blocks
        }


    }

    #region Level 2 attributes
  
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
            _worldHeight += 4;
            Object = null;
            SpawnRaiser();
        }


        if (heightChangePoint >= spawnPoint - 2)
        {
            Object = null;
        }
    }
    void SpawnRaiser()
    {

        int randLane = Random.Range(_firstLane, _firstLane + 3);
        if (_worldBroken)
        {
            randLane = singleLane;
        }
        Instantiate(World[2], new Vector3(randLane + _offset, _worldHeight - 3, spawnPoint - 1), World[2].rotation, _parent);
        Instantiate(Triggers[2], new Vector3(_firstLane + 1 + _offset, 1.5f, spawnPoint + 1), Triggers[2].rotation, _parent); // the gone trigger

       ClearPath(3);

    }

    #endregion

    #region Level 3 attributes

    void BreakWorld()
    {
        int breakLength = Random.Range(15, 30);
        randNumber = Random.Range(_firstLane, _firstLane + 3);

        singleLane = randNumber;
        stopBreak = spawnPoint + breakLength;

        _worldBroken = true;
    }

    public void ForceBreak(int lane, int breakLength)
    {
        singleLane = _firstLane + (lane-1);
        stopBreak = spawnPoint + breakLength;
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

    public void ClearPath(int Length)
    {

        ClearDistance = spawnPoint + Length;

        _pathClear = false;
    }

    public Transform GetSpecificObject(int Category, int Item)
    {
        return Objects[Category, Item];
    }

    public Transform GetWorldBlocks(int index)
    {
        return World[index];
    }

    public void SpawnPlatform(int spawnPoint, bool TransitioningToBoss)
    {
        if (TransitioningToBoss)
        {
            Instantiate(World[3], new Vector3(0f, _worldHeight, spawnPoint + 2), World[3].rotation);
            Instantiate(World[4], new Vector3(_firstLane + 1, _worldHeight+ 0.5001f, spawnPoint + 2), World[4].rotation);
        }else
        {
            Instantiate(World[3], new Vector3(0f, _worldHeight, spawnPoint + 2), Quaternion.Euler(0, 180, 0));
        }
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent.transform;
    }

    public void SetLanes(int firstLane, float offset)
    {
        _firstLane = firstLane;
        _offset = offset;
    }

    public void SetSpawnPoint(int SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }
    public void SetWorldHeight(int worldHeight)
    {
        _worldHeight = worldHeight;
    }

}