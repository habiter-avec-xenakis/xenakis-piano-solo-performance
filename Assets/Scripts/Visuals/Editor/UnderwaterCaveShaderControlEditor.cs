using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnderwaterCaveControl))]
public class UnderwaterCaveShaderControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UnderwaterCaveControl ucsc = (UnderwaterCaveControl)target;

        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Time", ucsc.time.ToString());
        EditorGUI.EndDisabledGroup();

        Repaint();
    }
}
