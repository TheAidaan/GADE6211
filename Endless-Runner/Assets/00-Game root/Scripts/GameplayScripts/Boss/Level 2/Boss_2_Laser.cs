using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Laser : MonoBehaviour
{
    Transform _player;
    [SerializeField] Transform _aim;
    bool _active;

    public GameObject m_shotPrefab;

    RaycastHit hit;

    void Update()
    {
        if (!GameManager.characterDeath && _active)
        {
            transform.LookAt(new Vector3(_player.position.x, _player.position.y, _player.position.z + 1));
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Activate(Transform player)
    {
        _player = player;
        _active = true;
    }

    void Shoot()
    {
  
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4f )) 
        {
            Debug.Log(hit);
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<Boss_2_Ammo>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);
        }

    }

}

