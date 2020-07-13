using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Animator : MonoBehaviour
{
    Animator anim;
    Boss_3_Manager manager;

    string boolName;
    bool _activeState;
    int _presetIndex;

    static bool _animated;
    public static bool Animated { get { return _animated; } }

    [SerializeField] GameObject[] Raisers = new GameObject[10];
    [SerializeField] GameObject Platform;
    [SerializeField] GameObject[] AnmationTriggers = new GameObject[2];

    void Start()
    {
        Platform.SetActive(false);
        _presetIndex = 1;
        anim = GetComponent<Animator>();
        manager = GetComponentInParent<Boss_3_Manager>();
        DeactivateAllRaisers();
        SetTriggers();
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

        _animated = _activeState;
        manager.IncreaseStage();
    }

    public void FinalAnimation()
    {
        DeactivateAllRaisers();
        anim.SetBool("End_Boss_3", true);

        Raisers[9].SetActive(true);

        AnmationTriggers[0].SetActive(false);
        AnmationTriggers[1].SetActive(true);
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
            
        }
        else
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

    public void ActivatePlatform()
    {
        Platform.SetActive(true);
    }

    public void SetTriggers()
    {
        if (Boss_3_Manager.ClockwiseRotation)
        {
            AnmationTriggers[0].SetActive(true);
            AnmationTriggers[1].SetActive(false);

        }
        else
        {
            AnmationTriggers[0].SetActive(false);
            AnmationTriggers[1].SetActive(true);
        }

    }
}
