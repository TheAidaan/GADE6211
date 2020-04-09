using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    KeyCode moveR = KeyCode.D;
    KeyCode moveL = KeyCode.A;
    KeyCode jump = KeyCode.Space;

    enum Lanes { Left = 1, Center, Right};
    Lanes lane = Lanes.Center;

    bool _jumpLock = false;
    bool _controlLock=false;
    bool _stopForward = false;
    bool _SuperSize;

    CharacterReact React;

    float yPos = 1;
    float zJumpAdj = 0;

    private void Start()
    {
        React = GetComponent<CharacterReact>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stopForward==false)
        {
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        }
               
        if ((Input.GetKey(moveL)) && ((int)lane > 1)&& (_controlLock==false))
        {
            lane -= 1;
            zJumpAdj = 0;
            yPos = transform.position.y;
            StartCoroutine(ControlLock());
            Move();

        }

        if ((Input.GetKey(moveR)) && ((int)lane < 3) && (_controlLock==false))
        {    
            lane += 1;
            zJumpAdj = 0;
            StartCoroutine(ControlLock());
            yPos = transform.position.y;
            Move();

        }

        if ((Input.GetKey(jump)) && (_jumpLock == false))
        {
            yPos = 2f;
            zJumpAdj = 1f;
            Move();
            StartCoroutine(stopJump());
            _jumpLock = true;
        }

        if ((transform.position.y<1.2f)&&(_SuperSize==false))
        {
            _jumpLock = false;
        }
    }
    public Vector3 goTo()
    {
        switch ((int)lane)
        {
            case 1: return new Vector3( -1, yPos, transform.position.z + zJumpAdj);
            case 2: return new Vector3( 0, yPos, transform.position.z + zJumpAdj);
            case 3: return new Vector3( 1, yPos, transform.position.z + zJumpAdj);
            default: return transform.position;
        }
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
        zJumpAdj = 1;
        Move();
        zJumpAdj = 0;
    }

    public void superSized(bool enable)
    {
        lane = Lanes.Center;
        Move();
        _controlLock = enable;
        _jumpLock = enable;
        _SuperSize = enable;

    }

    public void Hole()
    {
        _jumpLock = true;
        _controlLock = true;
        _stopForward = true;

        yPos = -10f;

    }

    public void Fling(bool enable)
    {
        _jumpLock = enable;

        if (enable == true)
        {
            yPos = 6f;
            zJumpAdj = 5f;
            Move();
        }
        else
        {
            yPos = .9f;
            zJumpAdj = 5f;
            Move();
        }
    }

    public void StopForward()
    {
        _stopForward = true;
    }
    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, goTo(), 1.5f);
    }

}
