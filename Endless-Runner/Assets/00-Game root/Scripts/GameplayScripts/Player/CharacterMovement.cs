using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Vector3 movement;
    bool MoveR, MoveL, Jump;

    enum Lanes { Left = 1, Center, Right };
    Lanes lane;

    bool _controlLock = false;
    bool _jumpLock =  false;
    bool _stopForward = false;


    bool _fling, _endFling;
    bool _superSize;
    

    Rigidbody rb;
    CharacterReact React;

    int _jumpCount, _maxJump;
    int currentLevel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        React = GetComponentInChildren<CharacterReact>();

        lane = Lanes.Center;
        if (currentLevel == 3)
        {
            _maxJump = 2;
        }else
        {
            _maxJump = 1;
        }
    }
    private void FixedUpdate()
    {
        Move();
        rb.velocity = new Vector3(0, movement.y, movement.z);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A)) && ((int)lane > 1) && (_controlLock == false))
        {
           MoveL = true;
        }

        if ((Input.GetKeyDown(KeyCode.D)) && ((int)lane < 3) && (_controlLock == false))
        {
            MoveR = true;
        } 


        if ((Input.GetKeyDown(KeyCode.Space)) && (_jumpLock == false) && ( (OnGround() == true) || (_jumpCount < _maxJump)))
        {
            Jump = true;
            _jumpCount++;

        }

        if (OnGround() == true) 
        {
            _jumpCount = 0;
           
            if(_endFling)
            {
                _endFling = false;
                _jumpLock = false;
                React.endFling();

            }
          
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

        if ((OnGround() == false) &&(_superSize==false))
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
        _superSize = enable;

    }

    public void Hole()
    {
        StopMovement();
        rb.AddForce(Vector3.down * 5000);
    }

    public void Fling()
    {
        _fling = true;
        _jumpLock = true;
    }

   bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f);
    }

    public void SetLevel(int Level)
    {
        currentLevel = Level;
    }
    public void StopMovement()
    {
        _stopForward = true;
        _jumpLock = true;
        _controlLock = true;
    }

}
