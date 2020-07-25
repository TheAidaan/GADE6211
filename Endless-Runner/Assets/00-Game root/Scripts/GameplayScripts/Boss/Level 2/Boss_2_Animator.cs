using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Animator : MonoBehaviour
{
    enum Lanes { Right=0, Middle, Left }
    Lanes RandLane;
    const string prefix = "Prep_";

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TestSmash()
    {
        anim.SetTrigger("Prepare");

        anim.SetTrigger(prefix + "Middle");

        anim.SetTrigger("Smash");
    }

    public IEnumerator Smash()
    {
        anim.SetTrigger("Prepare");

        RandLane = (Lanes)Random.Range(0, 3);
        anim.SetTrigger(prefix + RandLane);

        yield return new WaitForSeconds(1.5f);
            
        anim.SetTrigger("Smash");

       
    }
}
