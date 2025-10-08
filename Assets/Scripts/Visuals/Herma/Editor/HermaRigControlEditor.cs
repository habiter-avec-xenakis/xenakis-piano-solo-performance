using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HermaRigControl))]
public class HermaRigControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var rigControl = (HermaRigControl)target;
        base.OnInspectorGUI();
        GUILayout.Space(20);

        if(rigControl.rigPresets == null)
        {
            return;
        }

        for (int i = 0; i < rigControl.rigPresets.Length; i++)
        {
            if (GUILayout.Button("Preset " + i))
            {
                rigControl.ApplyPreset(i);
            }
        }
    }
}