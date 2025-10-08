using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivation : MonoBehaviour
{
    private void Start()
    {
        for(int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        //PerformanceGlobalManager.onControlsDisplayChanged.AddListener(PerformanceGlobalManager.OnControlsDisplayChanged);
    }
}