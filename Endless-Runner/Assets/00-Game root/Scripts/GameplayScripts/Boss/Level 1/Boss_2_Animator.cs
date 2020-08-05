using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Animator : MonoBehaviour
{
    enum Lanes { Right=0, Middle, Left }
    Lanes RandLane;
    const string prefix = "Prep_";

    Animator anim;
    Boss_2_Manager manager;
    void Start()
    {
        manager = GetComponentInParent<Boss_2_Manager>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator Smash()
    {
        anim.SetTrigger("Prepare");

        RandLane = (Lanes)Random.Range(0, 3);
        anim.SetTrigger(prefix + RandLane);

        yield return new WaitForSeconds(1.5f);

        anim.SetBool("Smash", true);

        yield return new WaitForSeconds(1f);
        anim.SetBool("Smash", false);
        manager.StartAttacking();


    }
    public void Aim()
    {
        anim.SetTrigger("Aim");
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
    }
}
