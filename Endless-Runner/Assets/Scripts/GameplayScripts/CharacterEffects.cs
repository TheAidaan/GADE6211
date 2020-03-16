using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public static bool resistant;

    public static Material orange;
    public static Material blue;

    public static Renderer Object;

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
        if (other.gameObject.name == "PowerUp(clone)")
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
}
