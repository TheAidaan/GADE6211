using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterMovement : MonoBehaviour
{
    Vector3 movement;
    bool MoveR, MoveL, Jump;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    private void FixedUpdate()
    {
        Move();
        rb.velocity = movement;
    }
    void Move()
    {
        //transform.RotateAround(target.position, Vector3.up, 20 * Time.deltaTime);

        movement = Vector3.zero;

        if (MoveR)
        {
            rb.AddForce(Vector3.right * 2500);
            MoveR = false;
        }
        if (MoveL)
        {
            rb.AddForce(Vector3.left * 2500);
            MoveL = false;
        }
    }
        // Update is called once per frame
    void Update()
    {        

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveL = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveR = true;
        }
    }

  
}
