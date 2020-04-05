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

    float yPos = 1;
    float zJumpAdj = 0;

    // Update is called once per frame
    void Update()
    {
        if (_stopForward==false)
        {
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        }
        if (transform.position.y <0)
        {
            GameManager.characterDeath = true;
        }
               
        if ((Input.GetKey(moveL)) && ((int)lane > 1)&& (_controlLock==false))
        {
            lane -= 1;
            zJumpAdj = 0;
            yPos = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);

        }

        if ((Input.GetKey(moveR)) && ((int)lane < 3)&&(_controlLock==false))
        {    
            lane += 1;
            zJumpAdj = 0;
            yPos = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);
            
        }

        if ((Input.GetKey(jump)) && (_jumpLock == false))
        {
            yPos = 2f;
            zJumpAdj = 1f;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);
            StartCoroutine(stopJump());
            _jumpLock = true;
        }

        if (transform.position.y<1.2f)
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

    IEnumerator stopJump()
    {       
        yield return new WaitForSeconds(.3f);
        yPos = .9f;
        zJumpAdj = 1;
        transform.position = Vector3.Lerp(transform.position, goTo(), 3f);
        zJumpAdj = 0;
    }

    public void superSized(bool enable,float newHeight)
    {
        yPos = newHeight;
        if (enable)
        {
            lane = Lanes.Center;
            _controlLock = true;
            _jumpLock = true;
            
            transform.position = goTo();
        }else
        {
            _controlLock = false;
            _jumpLock = false;
            transform.position = goTo();
        }
        
    }

    public void StopForward(bool stop)
    {
        _stopForward = stop;

    }   

}
