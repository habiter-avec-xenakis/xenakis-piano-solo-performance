//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(RawImageWebcam))]
//public class RawImageWebcamEditor : Editor
//{
//    private RawImageWebcam rawImageWebcam;
//    private string currentWebcamName;

//    private void OnEnable()
//    {
//        rawImageWebcam = (RawImageWebcam)target;
//        currentWebcamName = PerformanceGlobalManager.GetIniFileParameterString("Webcam");
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        EditorGUILayout.BeginHorizontal();

//        if (GUILayout.Button("Refresh"))
//        {
//            currentWebcamName = PerformanceGlobalManager.GetIniFileParameterString("FacemotionIP");
//        }

//        if (currentWebcamName == "")
//        {
//            EditorGUILayout.LabelField("NONE");
//        }

//        else
//        {
//            EditorGUILayout.LabelField("Current name in file " + currentWebcamName);
//        }

//        EditorGUILayout.EndHorizontal();

//        if (GUILayout.Button("Save to file"))
//        {
//            PerformanceGlobalManager.SetIniFileParameter("Webcam", rawImageWebcam.webcamName);
//        }

//        EditorGUI.BeginDisabledGroup(currentWebcamName == "");

//        if (GUILayout.Button("Load from file"))
//        {
//            rawImageWebcam.webcamName = PerformanceGlobalManager.GetIniFileParameterString("Webcam");
//            EditorUtility.SetDirty(rawImageWebcam);
//        }

//        EditorGUI.EndDisabledGroup();
//    }
//}