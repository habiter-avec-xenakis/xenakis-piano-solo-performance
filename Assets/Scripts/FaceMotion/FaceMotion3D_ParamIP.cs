using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityRecieve_FACEMOTION3D))]
public class FaceMotion3D_ParamIP : MonoBehaviour
{
    private UnityRecieve_FACEMOTION3D fm3d;
    public bool useIPFromFile;

    private void Awake()
    {
        fm3d = GetComponent<UnityRecieve_FACEMOTION3D>();

        if(useIPFromFile)
        {
            LoadParameter();
        }
    }

    public void SaveParameter()
    {
        PerformanceGlobalManager.SetIniFileParameter("FacemotionIP", GetComponent<UnityRecieve_FACEMOTION3D>().iOS_IPAddress);
    }

    public void LoadParameter()
    {
        var value = PerformanceGlobalManager.GetIniFileParameterString("FacemotionIP");

        if(value != "")
        {
            GetComponent<UnityRecieve_FACEMOTION3D>().iOS_IPAddress = value;
        }
    }
}