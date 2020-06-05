using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Transform Player;

    static bool _bossActive = false;
    static bool _endBoss;
    public static bool bossActive { get { return _bossActive; } }
    public static bool endBoss { get { return _endBoss; } }

    public virtual void Start()
    {
        _bossActive = false;
        _endBoss = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
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
            BossStart();
        }else
        {
            BossEnd();
        }
    }

    public virtual void BossStart() {}

    public void BossDeactivation(){ _bossActive = false; _endBoss = false; }

    public virtual void BossEnd() { }

}
