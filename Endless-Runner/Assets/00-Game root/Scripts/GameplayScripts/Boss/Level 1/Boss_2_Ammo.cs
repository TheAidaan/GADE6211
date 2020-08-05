using UnityEngine;
using System.Collections;

public class Boss_2_Ammo : Obstacle
{
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = gameObject.transform.TransformDirection(Vector3.forward * 30f);
    }

    public override void DestroyCondition() {    }
}