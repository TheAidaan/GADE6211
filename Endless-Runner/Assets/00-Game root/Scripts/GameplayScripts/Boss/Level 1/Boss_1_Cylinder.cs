using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Cylinder : MonoBehaviour
{
    int _nextStageHeight = -300;
    bool _fall = true;
    bool _inPosition = false;
    void Update()
    {
        if (BossManager.bossActive)
        {
            if (_fall)
            {
                transform.Translate(Vector3.down * DescendSpeed());
                if (transform.position.y < _nextStageHeight)
                {
                    GetComponentInParent<Boss_1_Manager>().IncreaseStage();
                    _nextStageHeight -= 150;

                    if (_nextStageHeight == -720)
                    {
                        _fall = false;
                    }

                    if (_nextStageHeight == -750)
                    {
                        _nextStageHeight = -720;
                    }
                }

            }else
            {
                if (!_inPosition)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -718.8f, transform.position.z), 0.02f);
                    _inPosition = true;
                }
            }
        }


    }

    public float DescendSpeed()
    {
        switch(Boss_1_Manager.CurrrentStage)
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
