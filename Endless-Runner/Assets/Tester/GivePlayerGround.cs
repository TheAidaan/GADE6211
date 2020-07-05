using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerGround : MonoBehaviour
{
    bool _assignGround;
    RaycastHit _hit;

    private void Start()
    {
        _assignGround = true;
    }
    private void Update()
    {
        if (_assignGround)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out _hit, .5f, LayerMask.GetMask("Player")))
            {
                transform.SetParent(_hit.transform);
                _assignGround = false;
            }

        }
        
   
    }
}
