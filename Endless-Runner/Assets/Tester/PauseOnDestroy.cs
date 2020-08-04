using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        Time.timeScale = 0;
        Debug.Log(transform.position);
    }
}
