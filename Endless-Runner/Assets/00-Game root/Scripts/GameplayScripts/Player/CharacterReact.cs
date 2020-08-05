using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReact : MonoBehaviour
{
    int _flingCount, superSizeCount, _immunityCount;

    Material[] materials = new Material[6];
    Renderer rend;

    CharacterAnimations animator;
    CharacterMovement Movement;
    CharacterAbility ability;
    GameUI UI;

    bool _flingActive;


    enum playerResistance { none, simple, timeBased }

    playerResistance CurrentResistanceLevel;
    playerResistance PreviousResistanceLevel;

    void Start()
    {
        _flingCount = 0;
        superSizeCount = 0;
        _immunityCount = 0;
        
        animator = GetComponent<CharacterAnimations>();
        ability = GetComponentInParent<CharacterAbility>();
        Movement = GetComponentInParent<CharacterMovement>();
        UI = FindObjectOfType<GameUI>();

        CurrentResistanceLevel = playerResistance.none;
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials = Resources.LoadAll<Material>("Materials/Player");
        UI.ChangeColour(materials[0]);
    }

    private void UpdateFling()
    {
        _flingCount++;
        print("Fling has been activated " + _flingCount + " times");
        GameManager.gameManager.updateMetrics -= UpdateFling;
    }
    private void UpdateImmunity()
    {
        _immunityCount++;
        print("Immunity has been activated " + _immunityCount + " times");
        GameManager.gameManager.updateMetrics -= UpdateImmunity;
    }
    private void UpdateSuperSize()
    {
        superSizeCount++;
        print("SuperSize has been activated " + superSizeCount + " times");
        GameManager.gameManager.updateMetrics -= UpdateSuperSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            CurrentResistanceLevel = playerResistance.timeBased;
        }
    }

    void ChangeMaterial(int MaterialIndex)
    {
        rend.sharedMaterial = materials[MaterialIndex];
        UI.ChangeColour(materials[MaterialIndex]);
    }

    public int PlayerResistance()
    {
        return (int)CurrentResistanceLevel;
    }


    #region Fling Reaction
    public void Fling()
    {
        SoundManager.PlaySoundEffect(3);
        GameManager.gameManager.updateMetrics += UpdateFling;

        _flingActive = true;

        PreviousResistanceLevel = CurrentResistanceLevel;
        CurrentResistanceLevel = playerResistance.timeBased;

        ChangeMaterial(3);

        Movement.Fling();
    }
    public void EndFling()
    {
        ability.Shoot();

        _flingActive = false;

        CurrentResistanceLevel = PreviousResistanceLevel;
        ChangeMaterial((int)CurrentResistanceLevel);
    }
    public void Bounce(Vector3 force)
    {
        if (_flingActive)
        {
            EndFling();
            Movement.Bounce(force);
        }
    }

    #endregion

    #region SuperSize Reaction 

    public void SuperSize()
    {
        GameManager.gameManager.updateMetrics += UpdateSuperSize;
        SoundManager.PlaySoundEffect(4);
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

    public void Transition()
    {
        Movement.Transition();

    }

    public void Immunity()
    {
        SoundManager.PlaySoundEffect(2);
        GameManager.gameManager.updateMetrics += UpdateImmunity;

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
        Die(false, true);
    }


    public void Die(bool killCharacter, bool stopMovement)
    {
        GameManager.characterDeath = true;
        
        if (stopMovement)
        {
            Movement.StopAllMovement();
        }else
        {
            Movement.LockControls(true);
        }

        if (killCharacter)
        {
            Destroy(transform.parent.gameObject);
        }
        
    }

    public Material CurrentMaterial()
    {
        return rend.sharedMaterial;
    }

    public void Gone()
    {
        Die(false,false);
    }
}
