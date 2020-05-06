﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReact : MonoBehaviour
{
    Material[] materials = new Material[6];
    Renderer rend;

    CharacterAnimations animator;
    CharacterMovement Movement;
    CharacterAbility ability;


    enum playerResistance { none, simple, timeBased }

    playerResistance CurrentResistanceLevel;
    playerResistance PreviousResistanceLevel;

    void Start()
    {
        animator = GetComponent<CharacterAnimations>();
        ability = GetComponentInParent<CharacterAbility>();
        Movement = GetComponentInParent<CharacterMovement>();

        CurrentResistanceLevel = playerResistance.none;
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials = Resources.LoadAll<Material>("Materials/Player");
       
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
        animator.Fling(true);

        PreviousResistanceLevel = CurrentResistanceLevel;
        CurrentResistanceLevel = playerResistance.timeBased;
        ChangeMaterial(3);

        Movement.Fling();
    }
    public void endFling()
    {
        animator.Fling(false);

        CurrentResistanceLevel = PreviousResistanceLevel;
        ChangeMaterial((int)CurrentResistanceLevel);
    }

    #endregion

    #region SuperSize Reaction 

    public void SuperSize()
    {
        StartCoroutine(SuperSizeEffect());
    }
    IEnumerator SuperSizeEffect()
    {
        PreviousResistanceLevel = CurrentResistanceLevel;
        CurrentResistanceLevel = playerResistance.timeBased;
        ChangeMaterial(2);

        animator.SuperSize(true);
        Movement.superSized(true);

        yield return new WaitForSeconds(5f);

        EndSuperSize();
    }

    public void EndSuperSize()
    {
        ability.Shoot();

        Movement.superSized(false);
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
        Movement.Hole();
        Die(false);
    }


    public void Die(bool killCharacter)
    {
        GameManager.characterDeath = true;
        GetComponent<CharacterStats>().SendStats();
        Movement.StopMovement();
        if (killCharacter)
        {
            Destroy(transform.parent.gameObject);
        }
        
    }

    public Material CurrentMaterial()
    {
        return rend.sharedMaterial;
    }
}