using System.Collections;
using System.Collections.Generic;
using UnityEngine;
abstract class Objects
{
    public abstract void Immunity();
    public abstract void Fling();
    public abstract void SuperSize();
    #region Collector objects
    public abstract void Coin();
    #endregion
    public abstract void MovingObstacle();
}
public class ObjectEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
