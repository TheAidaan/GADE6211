using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Animations : MonoBehaviour
{
    Animator anim;
    Boss_3_Manager manager;

    string boolName;
    bool _activeState;
    int _presetIndex;

    [SerializeField] GameObject[] Raisers = new GameObject[9];

    void Start()
    {
        _presetIndex = 1;
        anim = GetComponent<Animator>();
        DeactivateAllRaisers();
        manager = GetComponentInParent<Boss_3_Manager>();
    }

   public void SetAnimationState()
   {
        if (_activeState)   //if model is animated 
        {
            _activeState = false; // then turn off the animation
        }
        else
        {
           _presetIndex = Random.Range(1, 3); // otherwise pick a random state
           _activeState = true;
        }
            
        boolName = "Preset_" + _presetIndex; // set the name of the bool that must be effected
        anim.SetBool(boolName, _activeState);  // update the given preset
        ActivateRaisers();
        //Boss_3_RaiserConditions.CheckToActivate(_presetIndex);
    }
    public void ReleasePlayer()
    {
        DeactivateAllRaisers();
        anim.SetBool("End_Boss_3", true);
        
    }

    void ActivateRaisers()
    {
        if (Boss_3_Manager.ClockwiseRotation)
        {
            switch (_presetIndex)
            {
                case 1:
                    Raisers[4].SetActive(_activeState);
                    Raisers[5].SetActive(_activeState);
                    break;
                case 2:
                    Raisers[6].SetActive(_activeState);
                    break;
                case 3:
                    Raisers[7].SetActive(_activeState);
                    Raisers[8].SetActive(_activeState);
                    break;
                default:
                    DeactivateAllRaisers();
                    break;

            }
        }else
        {
            switch (_presetIndex)
            {
                case 1:
                    Raisers[0].SetActive(_activeState);
                    Raisers[1].SetActive(_activeState);
                    break;
                case 2:
                    Raisers[2].SetActive(_activeState);
                    break;
                case 3:
                    Raisers[3].SetActive(_activeState);
                    break;
                default:
                    DeactivateAllRaisers();
                    break;

            }
            

        }

    }

    void DeactivateAllRaisers()
    {
        for (int i = 0; i < Raisers.Length; i++)
        {
            Raisers[i].SetActive(false);
        }
    }
}
