using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_PathController : MonoBehaviour
{
    bool _circleComplete;
    readonly GameObject[] _children = new GameObject[6];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;
        }

        _children[2].SetActive(false); //Exit platform
        _children[3].SetActive(false); //Second half of circle
    }

    public void CompleteCircle()
    {
        if (!_circleComplete)
        {
            _children[3].SetActive(true); //Second half of circle
            _children[0].SetActive(false);//Entry platform

            _circleComplete = true;
        }
    }

    public void ReleasePlayer()
    {
        _children[2].SetActive(true);//Exit platform
        Destroy(_children[1]); // delete the first half of circle
    }
}
