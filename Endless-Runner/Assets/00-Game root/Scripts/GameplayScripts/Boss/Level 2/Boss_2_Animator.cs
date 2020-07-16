using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Animator : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void isRunning()
    {
        anim.SetTrigger("isRunning");
    }
}
