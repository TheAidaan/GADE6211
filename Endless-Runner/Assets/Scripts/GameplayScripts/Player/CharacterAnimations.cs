using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations: MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SuperSize(bool enable)
    {
        anim.SetBool("SuperSize_Transition", enable);
    }
    public void Jump(bool enable)
    {
        anim.SetBool("Jump_Transition", enable);
    }

    public void Fling(bool enable)
    {
        anim.SetBool("Fling_Transition", enable);
    }
    public void Fall(bool enable)
    {
        anim.SetBool("Fall_Transition", enable);
    }

}
