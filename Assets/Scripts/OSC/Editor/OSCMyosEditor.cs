using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using UnityEditor;

[CustomEditor(typeof(OSCMyos))]
public class OSCMyosEditor : Editor
{
    private OSCMyos oscMyos;

    private void OnEnable()
    {
        oscMyos = (OSCMyos)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Left mean " + oscMyos.lForceMean.ToString("0.00"));
        EditorGUILayout.LabelField("Right mean " + oscMyos.rForceMean.ToString("0.00"));
        EditorGUILayout.EndHorizontal();

        Repaint();
    }
}