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
        movement = Vector3.zero;
        //movement.z = 5;

        if (MoveR)
        {
            rb.AddRelativeForce(Vector3.right * 2500);
            MoveR = false;
        }
        if (MoveL)
        {
            rb.AddRelativeForce(Vector3.left * 2500);
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
