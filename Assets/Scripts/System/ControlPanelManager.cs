using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlPanelManager : MonoBehaviour
{
    //public Camera[] camerasVisuals = new Camera[0];
    //public Canvas[] canvasVisuals = new Canvas[0];
    public GameObject uiControlsPanel;

    public GameObject[] objectsToDestroy = new GameObject[0];

    private void Awake()
    {
        //OnControlsDisplayChanged(PerformanceGlobalManager.controlsDisplay);
        //PerformanceGlobalManager.onControlsDisplayChanged.AddListener(OnControlsDisplayChanged);
        PerformanceGlobalManager.currentControlsPanel = uiControlsPanel;

        if(!PerformanceGlobalManager.systemInUse)
        {
            return;
        }

        if (uiControlsPanel)
        {
            uiControlsPanel.transform.SetParent(PerformanceGlobalManager.subSequencePanel.transform, false);
        }

        foreach(var go in objectsToDestroy)
        {
            Destroy(go);
        }
    }

//    private void OnControlsDisplayChanged(int index)
//    {
//#if UNITY_EDITOR
//        for (int i = 0; i < 2; i++)
//#else
//        for (int i = 0; i < Display.displays.Length; i++)
//#endif
//        { 
//            if (i != index)
//            {
//                foreach(var cam in camerasVisuals)
//                {
//                    cam.targetDisplay = i;
//                }

//                foreach(var canvas in canvasVisuals)
//                {
//                    canvas.targetDisplay = i;
//                }
//            }
//        }
//    }
}
