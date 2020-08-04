using UnityEngine;

public class Boss_3_SpawnerMovement : Movement_Abstract
{
    Boss_3_Manager _manager;
    Rigidbody _rb;

    void Start()
    {
        _manager = GetComponentInParent<Boss_3_Manager>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!OnGround())
        {
            _rb.velocity = Vector3.down * 6;
            //_manager.SpawnerNotGrounded();
        }
        else
        {
            //_manager.SpawnerGrounded();
        }

        Move();
    }

    public bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f, LayerMask.GetMask("Ground"));
    }
}
