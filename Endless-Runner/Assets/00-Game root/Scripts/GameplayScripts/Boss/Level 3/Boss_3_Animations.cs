using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Animations : MonoBehaviour
{
    Animator anim;

    string boolName;
    bool _activeState;
    int _presetIndex;
    // Start is called before the first frame update
    void Start()
    {
        _presetIndex = 1;
        anim = GetComponent<Animator>();
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
            
        boolName = "Preset_" + _presetIndex; 
        anim.SetBool(boolName, _activeState);  
   }
    public void ReleasePlayer()
    {
        boolName = "Preset_" + _presetIndex;
        anim.SetBool(boolName, false);
        anim.SetBool("End_Boss_3", true);

    }

}
