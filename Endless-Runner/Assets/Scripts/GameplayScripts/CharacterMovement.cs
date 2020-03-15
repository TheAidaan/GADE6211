﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    KeyCode moveR = KeyCode.D;
    KeyCode moveL = KeyCode.A;
    KeyCode jump = KeyCode.Space;

    float horizVel;

    public Material orange;
    public Material blue;
    public Renderer Object;


    int laneNum;
    bool ControlLocked = false;

    private Rigidbody Self;

    bool resistant;



    // Start is called before the first frame update

    void Start()
    {
        horizVel = 0;
        laneNum = 2;
        Self = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
        Self.velocity = new Vector3(horizVel, GameManager.vertVel, 4 * GameManager.zVelAdj);

        if ((Input.GetKey(moveL)) && (laneNum > 1) && (ControlLocked == false))
        {
            horizVel = -2;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            ControlLocked = true;
        }

        if ((Input.GetKey(moveR)) && (laneNum < 3) && (ControlLocked == false))
        {
            horizVel = 2;
            StartCoroutine(stopSlide());
            laneNum += 1;
            ControlLocked = true;
        }

        if ((Input.GetKey(jump)) && (GameManager.vertVel == 0) && (ControlLocked == false))
        {
            GameManager.vertVel = 2;
            StartCoroutine(stopJump());
            ControlLocked = true;
        }
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        ControlLocked = false;
    }
    IEnumerator stopJump()
    {
        yield return new WaitForSeconds(.5f);
        GameManager.vertVel = -2;
        yield return new WaitForSeconds(.5f);
        GameManager.vertVel = 0;
        ControlLocked = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lethal")
        {
            if (resistant)
            {
                Destroy(other.gameObject);
                Object.material = orange;
                resistant = false;
            }
            else
            {
                GameManager.zVelAdj = 0;
                GameManager.characterDeath = true;
                Destroy(gameObject);

            }

        }
        if (other.gameObject.name == "PowerUp")
        {
            Destroy(other.gameObject);
            Object.material = blue;
            resistant = true;

        }
        if (other.gameObject.name == "Coin(Clone)")
        {
            GameManager.coinTotal += 1;
            Destroy(other.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RampTriggerBot")
        {
            GameManager.vertVel = 1f;

        }
        if (other.gameObject.name == "RampTriggerTop")
        {
            GameManager.vertVel = 0f;
        }
        if (other.gameObject.name == "Exit")
        {
            SceneManager.LoadScene("Complete");

        }

    }

}
