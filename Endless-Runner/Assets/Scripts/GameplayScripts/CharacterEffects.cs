using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public static bool resistant;

    public static Material orange;
        public static Material blue;

    public static Renderer Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lethal")
        {
            if (resistant)
            {
                Destroy(other.gameObject);
                Object.material = orange;
                resistant = false;
            }
            else
            {
                GameManager.zVelAdj = 0;
                GameManager.characterDeath = true;
                Destroy(gameObject);

            }

        }
        if (other.gameObject.name == "PowerUp")
        {
            Destroy(other.gameObject);
            Object.material = blue;
            resistant = true;

        }
        if (other.gameObject.name == "Coin(Clone)")
        {
            GameManager.coinTotal += 1;
            Destroy(other.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RampTriggerBot")
        {
            GameManager.vertVel = 1f;

        }
        if (other.gameObject.name == "RampTriggerTop")
        {
            GameManager.vertVel = 0f;
        }
       

    }
}
