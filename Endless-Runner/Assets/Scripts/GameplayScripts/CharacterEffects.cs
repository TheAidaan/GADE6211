using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    Material[] materials = new Material[6];
    Renderer rend;


    bool _resistant;
    bool _rangeResistant = false;

    bool _immunity, _superSize, _coin, _staticObstacle, _movingObstacle, _hole;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials = Resources.LoadAll<Material>("Materials");
    }

    private void OnTriggerEnter(Collider other)
    {
        _immunity = other.gameObject.GetComponent<ObjectEffects>().isImmunity();
        _superSize = other.gameObject.GetComponent<ObjectEffects>().isSuperSize();

        _coin = other.gameObject.GetComponent<ObjectEffects>().isCoin();

        _staticObstacle = other.gameObject.GetComponent<ObjectEffects>().isStaticObstacle();
        _movingObstacle = other.gameObject.GetComponent<ObjectEffects>().isMovingObstacle();
        _hole = other.gameObject.GetComponent<ObjectEffects>().isHole();


        if (_immunity)
        {
            if (_rangeResistant == false)
            {
                ImmunityCollided immunityCollided = new ImmunityCollided(other.gameObject);

                ChangeMaterial(1);
                _resistant = true;
            }
        }

        if (_coin)
        {
            CoinCollided coinCollided = new CoinCollided(other.gameObject);
        }

        if (_superSize)
        {
            if (_rangeResistant == false)
            {
                SuperSizeCollided superSize = new SuperSizeCollided(other.gameObject);
                StartCoroutine(resistantPeriod());
                ChangeMaterial(2);              
            }
            
        }

        if (_staticObstacle || _movingObstacle)
        {
            if (_rangeResistant == true)
            {
                ObstacleCollided obstacleCollided = new ObstacleCollided(other.gameObject, false);
            }
            else
            {
                if (_resistant == true)
                {
                    ObstacleCollided obstacleCollided = new ObstacleCollided(other.gameObject, false);
                    ChangeMaterial(0);
                    _resistant = false;
                }
                else
                {
                    ObstacleCollided obstacleCollided = new ObstacleCollided(gameObject, true);
                }

            }
            
        }

        if (_hole)
        {
            if (_rangeResistant == false)
            {
                holeCollided hole = new holeCollided(gameObject);
                GetComponent<CharacterMovement>().StopForward(true);
            }
        }

    }
    public void ChangeMaterial(int materialIndex)
    {
        rend.sharedMaterial = materials[materialIndex];

    }

    public void RangeResistant(bool resistance)
    {
        _rangeResistant = resistance;
    }

    IEnumerator resistantPeriod()
    {
        RangeResistant(true);
        transform.localScale = new Vector3(3f, 3f, 3f);
        GetComponent<CharacterMovement>().superSized(true, 2);

        yield return new WaitForSeconds(5f);

        GetComponent<CharacterMovement>().superSized(false, .9f);
        transform.localScale = new Vector3(.8f, .8f, .8f);

        ChangeMaterial(0);
        RangeResistant(false);


    }
}
