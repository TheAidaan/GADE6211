using UnityEngine;

public class Boss_3_SpawnerMovement : Movement_Abstract
{
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!OnGround())
        {
            _rb.velocity = Vector3.down * 6;
        }

        Move();
    }

    public bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f, LayerMask.GetMask("Ground"));
    }
}
