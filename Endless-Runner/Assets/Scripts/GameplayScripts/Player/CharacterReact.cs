using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReact : MonoBehaviour
{
    Material[] materials = new Material[6];
    Renderer rend;

    CharacterAnimations animator;

    CharacterMovement EffectMovement;

   enum playerResistance { none, simple, timeBased}

    playerResistance CurrentResistanceLevel;
    playerResistance PreviousResistanceLevel;

    //void Update()
    //{
    //    if ((Input.GetKey(KeyCode.Q)))
    //    {
    //        animator.Fall(true);
    //    }
    //}
    void Start()
    {
        CurrentResistanceLevel = playerResistance.none;
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials = Resources.LoadAll<Material>("Materials");
        animator = GetComponent<CharacterAnimations>();

        EffectMovement = GetComponentInParent<CharacterMovement>();
    }

    void ChangeMaterial(int MaterialIndex)
    {
        rend.sharedMaterial = materials[MaterialIndex];
    }

    public int PlayerResistance()
    {
        return (int)CurrentResistanceLevel;

    }


    #region Fling Reaction
    public void Fling()
    {
        
        StartCoroutine(FlingPlayer());
    }

    IEnumerator FlingPlayer()
    {
        //Work here
        animator.Fling(true);
        PreviousResistanceLevel = CurrentResistanceLevel;
        CurrentResistanceLevel = playerResistance.timeBased;
        ChangeMaterial(3);
        EffectMovement.Fling(true);

        yield return new WaitForSeconds(1f);

        EffectMovement.Fling(false);
        animator.Fling(false);
        CurrentResistanceLevel = PreviousResistanceLevel;
        ChangeMaterial((int)CurrentResistanceLevel);
        
    }
    #endregion

    #region SuperSize Reaction 

    public void SuperSize()
    {
        StartCoroutine(resistantPeriod());
    }
    IEnumerator resistantPeriod()
    {
        PreviousResistanceLevel = CurrentResistanceLevel;
        CurrentResistanceLevel = playerResistance.timeBased;
        ChangeMaterial(2);

        animator.SuperSize(true);
        EffectMovement.superSized(true);

        yield return new WaitForSeconds(5f);

        EffectMovement.superSized(false);
        animator.SuperSize(false);

        CurrentResistanceLevel = PreviousResistanceLevel;
        ChangeMaterial((int)CurrentResistanceLevel);

    }
    #endregion                          

    public void Immunity()
    {
        CurrentResistanceLevel = playerResistance.simple;
        ChangeMaterial(1);
    }

    public void Hit()
    {
        CurrentResistanceLevel = playerResistance.none;
        ChangeMaterial(0);

    }

    public void Hole()
    {
        animator.Fall(true);
        EffectMovement.Hole();
    }

    public void Die()
    {
        GameManager.characterDeath = true;
        GetComponent<CharacterStats>().SendStats();
        EffectMovement.StopMovement();
        Destroy(gameObject);
    }
}
