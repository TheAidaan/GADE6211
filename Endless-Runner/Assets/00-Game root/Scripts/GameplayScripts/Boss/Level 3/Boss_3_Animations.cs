using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_Animations : MonoBehaviour
{
    bool[] _norm = new bool[4];
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   void SetState()
    {
        string boolName;
        //for (int i=1 ; i<5 ; i++)
        //{
        int i = 1;
            boolName = "Norm_Raise_Q" + i;
            anim.SetBool(boolName, _norm[i - 1]);
        //}
        
    }
    public void NormRaiseQ(int Index)
    {
        _norm[Index - 1] = !_norm[Index - 1];
        SetState();

    }


}
