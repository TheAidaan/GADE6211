using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Lazer : MonoBehaviour
{
    Transform _player;
    bool _active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.characterDeath && _active)
        {
            transform.LookAt(_player);
        }
    }

    public void Activate(Transform player)
    {
        _player = player;
        _active = true;
    }

}
