using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    Vector3 movement;
    bool MoveR, MoveL, Jump;

    enum Lanes { Left = 1, Center, Right };
    Lanes  lane;

    bool _controlLock = false;
    bool _jumpLock =  false;
    bool _stopForward = false;

    float _forwardIncrease;

    bool _fling, _endFling, _superFling;
    bool _superSize;

    Rigidbody rb;
    CharacterReact React;

    int _jumpCount, _maxJump, _increaseSpeedPoint;

    private void Start()
    {
        _forwardIncrease = 1;
        _increaseSpeedPoint = 200;
        rb = GetComponent<Rigidbody>();
        React = GetComponentInChildren<CharacterReact>();

        lane = Lanes.Center;
        if (GameManager.CurrentLevel == 3)
        {
            _maxJump = 1;
        }else
        {
            _maxJump = 0;
        }
        _jumpCount = 0;

        GetComponentInChildren<CharacterAnimations>().Move(true);
    }
    private void FixedUpdate()
    {
        Move();
        rb.velocity = gameObject.transform.TransformDirection(movement);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A)) && ((int)lane > 1) && (!_controlLock))
        {
           MoveL = true;
        }

        if ((Input.GetKeyDown(KeyCode.D)) && ((int)lane < 3) && (!_controlLock))
        {
            MoveR = true;
        } 


        if ((Input.GetKeyDown(KeyCode.W)) && (!_jumpLock) && ( (OnGround()) || (_jumpCount < _maxJump)))
        {
            Jump = true;
            _jumpCount++;

        }

        if (OnGround()) 
        {
            _jumpCount = 0;
           
            if (_endFling)
            {
                _endFling = false;
                _jumpLock = false;
                React.EndFling();
            }
        }
       
    }

    void Move()
    {

        movement = Vector3.zero;

        if (_stopForward==false)
        {
            
            if (CharacterStats.Distance > _increaseSpeedPoint)
            {
                _forwardIncrease += .3f;
                _increaseSpeedPoint += 100;
            }
            movement.z = 5 * _forwardIncrease;

        }

        if (MoveR)
        {
            rb.AddRelativeForce(Vector3.right * 2500);
            MoveR = false;
            lane += 1;
        }
        if (MoveL)
        {
            rb.AddRelativeForce(Vector3.left * 2500);
            MoveL = false;
            lane -= 1;
        }

        if (Jump)
        {
            movement.y = 100f;

            if (_stopForward == false)
            {
                movement.z += 50f;
            }
            
            Jump = false;
        }

        if (!OnGround())
        {
            if (_superSize)
            {
                if (!SuperSizeGroundCheck())
                {
                    movement.y -= 6;
                }
                
            }else
            {
                movement.y -= 6;

            }
            
        }

        if (_fling)
        {
            movement.y = 500f;
            movement.z += 500f;
            _fling = false;
            _endFling = true;
        } 
        
        if (_superFling)
        {
            movement.y = 1500f;
            movement.z += 1500f;
            _superFling = false;
            _endFling = true;

        }
    }

    public void superSized(bool enable)
    {
        CenterPlayer();
        LockControls(enable);
    }

    public void Hole()
    {
        StopAllMovement();
        rb.AddForce(Vector3.forward * 1200);
    }

    public void Fling(bool Super)
    {
        if (Super)
        {
            _superFling = true;
        }
        else
        {
            _fling = true;
        }
       
        _jumpLock = true;
    }

    bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, .5f,LayerMask.GetMask("Ground") );
    }

    bool SuperSizeGroundCheck()
    {
        Vector3 rayLPos = new Vector3(transform.position.x - 1,transform.position.y,transform.position.z);
        Vector3 rayRPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        if (Physics.Raycast(transform.position, Vector3.down, .5f))
        {
            return true;
        }else
        {
            if(Physics.Raycast(rayLPos, Vector3.down, .5f))
            {
                return true;
            }
            else
            {
                if (Physics.Raycast(rayRPos, Vector3.down, .5f))
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }  

    }

    public void StopAllMovement()
    {
        _stopForward = true;
        _jumpLock = true;
        _controlLock = true;
    }
    public void StopForwardMovement(bool Active, bool Animate)
    {
        GetComponentInChildren<CharacterAnimations>().Move(Animate) ;
        _stopForward = Active;
    }

    public void LockControls(bool enable)
    {
        _controlLock = enable;
        _jumpLock = enable;
        _superSize = enable;
    }

    public void FakeCenter()
    {
        lane = Lanes.Center;
    }

    public void CenterPlayer()
    {
        switch ((int)lane)
        {
            case 1:
                rb.AddRelativeForce(Vector3.right * 2500);
                break;
            case 3:
                rb.AddRelativeForce(Vector3.left * 2500);
                break;
        }

        lane = Lanes.Center;
    }

    public void Bounce(Vector3 force)
    {
        Debug.Log("Bouncijg");
        rb.AddRelativeForce(force);

        _endFling = false;
        _jumpLock = false;
        _jumpCount = 0;
    }
}
