using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations: MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SuperSize(bool enable)
    {
        Debug.Log("animating");
        anim.SetBool("SuperSize_Forward", enable);
    }

    public void Fall(bool enable)
    {
        anim.SetBool("Fall_Transition", enable);
    }
    public void Move(bool enable)
    {
        anim.SetBool("isMoving_Transition", enable);
    }

}
