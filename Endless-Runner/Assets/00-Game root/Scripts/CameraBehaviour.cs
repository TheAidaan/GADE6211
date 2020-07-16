using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    Vector3 straightAhead_offset;
    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    
    public void Boss_2_CameraAdjustments()
    {
        CinemachineTransposer transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        straightAhead_offset = transposer.m_FollowOffset;
        vcam.LookAt = GameObject.FindGameObjectWithTag("Boss").transform;
        transposer.m_FollowOffset = new Vector3(0,2f, 8.290001f);
    }

    public void Boss_2_CameraReset()
    {
        CinemachineTransposer transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        vcam.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        transposer.m_FollowOffset = straightAhead_offset;
    }
}
