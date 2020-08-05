using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Gun : MonoBehaviour
{
    Transform _player;
    bool _active;
    bool _shooting;

    [SerializeField] Transform _ammo;
    Boss_2_Manager manager;
    Boss_2_Animator _animator;

    void Start()
    {
        manager = GetComponentInParent<Boss_2_Manager>();
        _animator = FindObjectOfType<Boss_2_Animator>();
    }
    void Update()
    {
        if (!GameManager.characterDeath && _active)
        {
            if (!_shooting)
            {
                transform.LookAt(new Vector3(_player.position.x, _player.position.y, _player.position.z + 1));
            }
            
        }
    }

    public void Activate(Transform player)
    {
        _player = player;
        _active = true;
    }

    public IEnumerator Shoot()
    {
        _shooting = true;

        yield return new WaitForSeconds(1f);
        _animator.Shoot();

        Instantiate(_ammo, transform.position, transform.rotation);

        _shooting = false;

        manager.StartAttacking();
    }


}

