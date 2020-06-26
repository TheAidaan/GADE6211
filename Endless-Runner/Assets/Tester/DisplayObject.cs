using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : MonoBehaviour
{
    public Transform Object;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Object, new Vector3(0, 1, 10), Object.rotation);
    }
}
