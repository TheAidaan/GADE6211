using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Cylinder : MonoBehaviour
{
    int _nextStageHeight = -300;
    void Update()
    {
        if (BossManager.bossActive)
        {
            transform.Translate(Vector3.down * DescendSpeed());
        }

        if (transform.position.y < _nextStageHeight)
        {
            GetComponentInParent<Boss_1_Manager>().IncreaseStage();
            _nextStageHeight -= 150;

            if (_nextStageHeight == -750)
            {
                _nextStageHeight = -719;
            }
        }
    }

    public float DescendSpeed()
    {
        switch(Boss_1_Manager.currrentStage)
        {
            case 0: 
                return 0.5f;
            case 1:
                return 0.1f;
            case 2:
                return 0.15f;
            case 3:
                return 0.2f;
            default:
                return 0f;
        }
    }


}
