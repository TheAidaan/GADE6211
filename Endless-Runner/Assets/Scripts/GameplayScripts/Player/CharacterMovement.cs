using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Vector3 movement;
    bool MoveR, MoveL, Jump;

    enum Lanes { Left = 1, Center, Right };
    Lanes lane = Lanes.Center;

    bool _jumpLock = false;
    bool _controlLock = false;
    bool _stopForward = false;

    bool _fling, _endFling;

    Rigidbody rb;
    CharacterReact React;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        React = GetComponentInChildren<CharacterReact>();
    }

    void FixedUpdate()
    {
        Move();
        rb.velocity = new Vector3(0, movement.y, movement.z);
        
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.A)) && ((int)lane > 1) && (_controlLock == false))
        {
            MoveL = true;
            StartCoroutine(ControlLock());
        }

        if ((Input.GetKey(KeyCode.D)) && ((int)lane < 3) && (_controlLock == false))
        {
            MoveR = true;
            StartCoroutine(ControlLock());
        }

        if ((Input.GetKey(KeyCode.Space)) && (_jumpLock == false)&& (OnGround() == true))
        {
            Jump = true;
            StartCoroutine(JumpLock());

        }

        if ((OnGround() == true) && (_endFling == true))
        {
            _jumpLock = false;
            _endFling = false;
            React.endFling();

        }
    }

    void Move()
    {
        movement = Vector3.zero;
        if (_stopForward==false)
        {
            movement.z = 5;
        }

        if (MoveR)
        {
            rb.AddForce(Vector3.right * 2500);
            MoveR = false;
            lane += 1;
        }
        if (MoveL)
        {
            rb.AddForce(Vector3.left * 2500);
            MoveL = false;
            lane -= 1;
        }

        if (Jump)
        {
            movement.y = 100f;
            movement.z += 50f;
            Jump = false;
        }

        if (OnGround() == false)
        {
            movement.y -= 6;
        }

        if (_fling)
        {
            movement.y = 500f;
            movement.z += 500f;
            _fling = false;
            _endFling = true;

        }
    }

    IEnumerator ControlLock()
    {
        _controlLock = true;
        yield return new WaitForSeconds(.3f);
        _controlLock = false;
    }
    IEnumerator JumpLock()
    {
        _jumpLock = true;
        yield return new WaitForSeconds(.4f);
        _jumpLock = false;
    }

    public void superSized(bool enable)
    {
        switch ((int)lane)
        {
            case 1:
                rb.AddForce(Vector3.right * 2500);
                break;
            case 3:
                rb.AddForce(Vector3.left * 2500);
                break;
        }

        lane = Lanes.Center;
        _controlLock = enable;
        _jumpLock = enable;

    }

    public void Hole()
    {
        StopMovement();
        rb.AddForce(Vector3.down * 5000);
    }

    public void Fling()
    {
        _jumpLock = true;
       // rb.AddForce(Vector3.up * 20000);
        _fling = true;
    }

   bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f);
    }

    public void StopMovement()
    {
        _stopForward = true;
        _jumpLock = true;
        _controlLock = true;
    }

}
