using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    int coinTotal;
    float distTotal;
    // Start is called before the first frame update
    void Start()
    {
        coinTotal = 0;
        distTotal = 0;
    }

    public void IncreaseCoins(int increment)
    {
        coinTotal += increment;
    }

    public int Coins()
    {
        return coinTotal;
    }

    public void IncreaseDistance(int increment)
    {
        distTotal += increment;
    }
}
