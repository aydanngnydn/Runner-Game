using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{ 
    public void SwitchPri(CinemachineVirtualCamera turnon, CinemachineVirtualCamera turnoff)
    {
        turnon.Priority = 100;
        turnoff.Priority = 1;
    }
}
