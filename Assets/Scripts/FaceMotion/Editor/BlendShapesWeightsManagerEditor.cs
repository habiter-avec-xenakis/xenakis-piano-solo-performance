using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlendShapesWeightsManager))]
public class BlendShapesWeightsManagerEditor : Editor
{
    private BlendShapesWeightsManager bswm;

    private void OnEnable()
    {
        bswm = (BlendShapesWeightsManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Acceleration", bswm.acceleration.ToString());
        EditorGUILayout.LabelField("Normalized Acceleration", bswm.normalizedAcceleration.ToString());

        Repaint();
    }
}
