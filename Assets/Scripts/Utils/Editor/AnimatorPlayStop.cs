using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Animator))]
public class AnimatorPlayStop : Editor
{
    public override void OnInspectorGUI()
    {
        Animator animator = (Animator)target;

        base.OnInspectorGUI();

        if(!Application.isPlaying)
        {
            return;
        }

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Play"))
        {
            animator.StopPlayback();
        }

        if (GUILayout.Button("Stop"))
        {
            animator.StartPlayback();
        }
        EditorGUILayout.EndHorizontal();
    }
}
