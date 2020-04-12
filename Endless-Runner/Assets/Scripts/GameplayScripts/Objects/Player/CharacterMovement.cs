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

    float yPos = .9f;
    float zJumpAdj;
    void FixedUpdate()
    {
        if (_stopForward == false)
        {
            Move();
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * 0);
        }
    }

    void Update()
    {

        if ((Input.GetKey(moveL)) && ((int)lane > 1) && (_controlLock == false))
        {
            lane -= 1;
            yPos = transform.position.y;
            StartCoroutine(ControlLock());


        }

        if ((Input.GetKey(moveR)) && ((int)lane < 3) && (_controlLock == false))
        {
            lane += 1;
            StartCoroutine(ControlLock());
            yPos = transform.position.y;


        }

        if ((Input.GetKey(jump)) && (_jumpLock == false))
        {
            yPos = 2f;
            zJumpAdj = .075f;
            StartCoroutine(stopJump());
            _jumpLock = true;
        }

        if ((transform.position.y < 1.2f) && (_SuperSize == false))
        {
            _jumpLock = false;
        }
    }
    public Vector3 goTo()
    {
        switch ((int)lane)
        {
            case 1: return new Vector3(-1, yPos, transform.position.z + zJumpAdj);
            case 2: return new Vector3(0, yPos, transform.position.z + zJumpAdj);
            case 3: return new Vector3(1, yPos, transform.position.z + zJumpAdj);
            default: return transform.position;
        }
    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, goTo(), 1.5f);
    }

    IEnumerator ControlLock()
    {
        _controlLock = true;
        yield return new WaitForSeconds(.3f);
        _controlLock = false;
    }
    IEnumerator stopJump()
    {
        yield return new WaitForSeconds(.3f);
        yPos = .9f;
        zJumpAdj = 0;
    }

    public void superSized(bool enable)
    {
        lane = Lanes.Center;
        _controlLock = enable;
        _jumpLock = enable;
        _SuperSize = enable;

    }

    public void Hole()
    {
        StopMovement();
        yPos = -10f;
        Move();
    }

    public void Fling(bool enable)
    {
        _jumpLock = enable;

        if (enable == true)
        {
            yPos = 6f;
        }
        else
        {
            yPos = .9f;
        }
    }

    public void StopMovement()
    {
        _stopForward = true;
        _jumpLock = true;
        _controlLock = true;
    }
  

}
