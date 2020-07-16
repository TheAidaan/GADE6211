using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSettings : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CameraBehaviour>().Boss_2_CameraAdjustments();
    }
}
