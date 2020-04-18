using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    KeyCode moveR = KeyCode.D;
    KeyCode moveL = KeyCode.A;
    KeyCode jump = KeyCode.Space;

    enum Lanes { Left = 1, Center, Right };
    Lanes lane = Lanes.Center;

    bool _jumpLock = false;
    bool _controlLock = false;
    bool _stopForward = false;

    bool _SuperSize;

    bool Right, Left,Jump;

    Rigidbody Self;

    private void Start()
    {
        Self = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (_stopForward == false)
        {
            Move();
            Self.velocity = Vector3.forward * 4; //  tyr the translate 
        }
        else
        {
            Self.velocity = Vector3.forward * 0;
        }
    }

    void Update()
    {
        //if (Self.velocity.y < 0)
        //{
        //    Self.velocity += Vector3.up * Physics.gravity.y * 1.5f;
        //}

        if ((Input.GetKey(moveL)) && ((int)lane > 1) && (_controlLock == false))
        {
            Left = true;
            StartCoroutine(ControlLock());
        }

        if ((Input.GetKey(moveR)) && ((int)lane < 3) && (_controlLock == false))
        {
            Right = true;
            StartCoroutine(ControlLock());
        }

        if ((Input.GetKey(jump)) && (_jumpLock == false))
        {
            Jump = true;
            
        }

    }

    void Move()
    {
        if (Right)
        {
            Self.AddForce(Vector3.right * 25000);
            Right = false;
            lane += 1;
        }
        if (Left)
        {
            Self.AddForce(Vector3.left * 25000);
            Left = false;
            lane -= 1;
        }
        if (Jump)
        {
            //Self.velocity = new Vector3(0, 70, 4);
            Self.AddForce(Vector3.up * 500,ForceMode.Impulse);
            Jump = false;
            StartCoroutine(JumpLock());
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
        Self.AddForce(Vector3.down * 500, ForceMode.Impulse);
        _jumpLock = false;
    }

    public void superSized(bool enable)
    { 
        switch ((int)lane)
        {
            case 1: Self.AddForce(Vector3.right * 25000);
                break;
            case 3:
                Self.AddForce(Vector3.left * 25000);
                break;
        }
        lane = Lanes.Center;
        _controlLock = enable;
        _jumpLock = enable;
        _SuperSize = enable;

    }

    public void Hole()
    {
        StopMovement();
        Self.velocity = Vector3.down * 75;
    }

    public void Fling(bool enable)
    {
        _jumpLock = enable;

        if (enable == true)
        {
            Self.AddForce(Vector3.up * 2500, ForceMode.Impulse);
        }
        else
        {
            Self.AddForce(Vector3.down * 2500, ForceMode.Impulse);
        }
    }

    public void StopMovement()
    {
        _stopForward = true;
        _jumpLock = true;
        _controlLock = true;
    }
    void Fall()
    {
        //if (Self.velocity.y < 0)
        //{
        //    Self.velocity += Vector3.up * Physics.gravity.y * 1.5f;
        //}
    }
  

}
