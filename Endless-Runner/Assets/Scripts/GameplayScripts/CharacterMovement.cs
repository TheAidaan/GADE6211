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

    bool jumpLock = false;
    float yPos = 1;
    float zJumpAdj = 0;

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * 4 * Time.deltaTime);
       
        if ((Input.GetKey(moveL)) && ((int)lane > 1))
        {
            lane -= 1;
            zJumpAdj = 0;
            yPos = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);

        }

        if ((Input.GetKey(moveR)) && ((int)lane < 3))
        {    
            lane += 1;
            zJumpAdj = 0;
            yPos = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);
            
        }

        if ((Input.GetKey(jump)) && (jumpLock == false))
        {
            yPos = 2.75f;
            zJumpAdj = 1f;
            transform.position = Vector3.Lerp(transform.position, goTo(), 3f);
            StartCoroutine(stopJump());
            jumpLock = true;
        }

        if (transform.position.y>.7)
        {
            jumpLock = false;
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
        yield return new WaitForSeconds(1f);
        yPos = 1;
        zJumpAdj = 0;
    }
    

}
