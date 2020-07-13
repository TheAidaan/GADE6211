using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossManager : MonoBehaviour
{
    public enum BOSS_STAGES { Start = 0, One, Two, Three, End }
    static BOSS_STAGES _currrentStage;

    bool _fetched;
    public static int CurrrentStage { get { return (int)_currrentStage; } }

    public Transform Player;
    public Spawner gameSpawner;

    public int spawnPoint;
    static bool _bossActive;
    static bool _endBoss;
    public static bool bossActive { get { return _bossActive; } }
    public static bool endBoss { get { return _endBoss; } }

    public virtual void Start()
    {
        _currrentStage = BOSS_STAGES.Start;
        _bossActive = false;
        _endBoss = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        gameSpawner = FindObjectOfType<Spawner>();
        gameSpawner.AssignObjects();
    }

    public void BossActivation()
    {
        if (!bossActive && !endBoss) // if the boss is inactive and the boss must not end: activate boss
        {
            _bossActive = true;
        }

        if (bossActive && endBoss) // if the boss is active and the boss must end: deactivate boss
        {
            _bossActive = false;
        }
        

        if (bossActive)
        {
            ActivateBoss();
        }else
        {
            DeactivateBoss();
        }
    }
    public virtual void IncreaseStage()
    {
        _currrentStage++;


        if (_currrentStage == BOSS_STAGES.End)
        {
            EndBoss();
        }
    }
    public void FetchPlayer()
    {
        if ((Player.position.z > (spawnPoint - 15)) && (!_fetched))
        {
            gameSpawner.SpawnBuildingBlocks(spawnPoint, null);

            spawnPoint++;

            if (spawnPoint == ((int)(transform.position.z - 51.98)))
            {
                _fetched = true;
            }
        }
    }

    public void GetSpawnPoint(int SpawnPoint)
    {
        spawnPoint = SpawnPoint;
    }

    public virtual void ActivateBoss() {}

    public virtual void EndBoss(){ _endBoss = true; }

    public virtual void DeactivateBoss() { _bossActive = false; }

}
