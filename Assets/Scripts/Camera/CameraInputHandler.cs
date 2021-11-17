using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraInputHandler : MonoBehaviour
{
    private CinemachineFreeLook cinemachineFreeLook;

    private void Start()
    {
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    
}
