using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReact : MonoBehaviour
{
    Material[] materials = new Material[6];
    Renderer rend;

   enum playerResistance { none, simple, timeBased}
    playerResistance ResistanceLevel;

    // Start is called before the first frame update
    void Start()
    {
        ResistanceLevel = playerResistance.none;
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials = Resources.LoadAll<Material>("Materials");

    }

    void ChangeMaterial(int materialIndex)
    {
        rend.sharedMaterial = materials[materialIndex];
    }

    #region Resistance Controllers
    public void setResistance(int resistanceLevel, int materialIndex)
    {
        switch(resistanceLevel)
        {
            case 1:
                ResistanceLevel = playerResistance.simple;
                GameManager.maySpawnObstacles = true;
                break;        
            case 2: ResistanceLevel = playerResistance.timeBased;
                GameManager.maySpawnObstacles = false;
                break;
            default: ResistanceLevel = playerResistance.none;
                GameManager.maySpawnObstacles = true;
                break;
        }

        ChangeMaterial(materialIndex);
    }

    public bool CheckResistance(bool checkRange)
    {
        if (checkRange)
        {
            if (((int)ResistanceLevel) > 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            if (((int)ResistanceLevel) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
   

    #endregion

    #region Fling Reaction
    public void Fling()
    {
        
        StartCoroutine(FlingPlayer());
    }

    IEnumerator FlingPlayer()
    {

        setResistance(2, 3);

        transform.position = Vector3.Lerp(transform.position, GoTo(transform.position.y + 6), 3f);
        yield return new WaitForSeconds(1f);
        transform.position = Vector3.Lerp(transform.position, GoTo(0.9f), 3f);

        setResistance(0, 0);


    }
    Vector3 GoTo(float height)
    {
       return new Vector3(transform.position.x, height, transform.position.z + 5);
        
    }
    #endregion

    #region SuperSize Reaction

    public void SuperSize()
    {
        StartCoroutine(resistantPeriod());
    }
    IEnumerator resistantPeriod()
    {
        transform.localScale = new Vector3(3f, 3f, 3f);
        GetComponent<CharacterMovement>().superSized(true, 2);

        yield return new WaitForSeconds(5f);

        GetComponent<CharacterMovement>().superSized(false, .9f);
        transform.localScale = new Vector3(.8f, .8f, .8f);

        setResistance(0, 0);

    }
    #endregion
}
