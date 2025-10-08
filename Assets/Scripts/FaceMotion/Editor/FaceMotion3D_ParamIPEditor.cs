using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FaceMotion3D_ParamIP))]
public class FaceMotion3D_ParamIPEditor : Editor
{
    private FaceMotion3D_ParamIP paramIP;
    private string currentFileIP;

    private void OnEnable()
    {
        paramIP = (FaceMotion3D_ParamIP)target;
        currentFileIP = PerformanceGlobalManager.GetIniFileParameterString("FacemotionIP");
    }

    public override void OnInspectorGUI()
    {
        currentFileIP = PerformanceGlobalManager.GetIniFileParameterString("FacemotionIP");

        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Refresh"))
        {
            currentFileIP = PerformanceGlobalManager.GetIniFileParameterString("FacemotionIP");
        }

        if (currentFileIP == "")
        {
            EditorGUILayout.LabelField("NONE");
        }

        else
        {
            EditorGUILayout.LabelField("Current IP in file " + currentFileIP);
        }

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Save to file"))
        {
            paramIP.SaveParameter();
        }

        EditorGUI.BeginDisabledGroup(currentFileIP == "");

        if (GUILayout.Button("Load from file"))
        {
            paramIP.LoadParameter();
            EditorUtility.SetDirty(paramIP);
        }

        EditorGUI.EndDisabledGroup();
    }
}
