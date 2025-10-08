using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProjectorSync))]
public class ProjectorSyncEditor : Editor
{
    private ProjectorSync pSync;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if(EditorGUI.EndChangeCheck())
        {
            pSync.SetCamera();
            if(pSync.uiBlackStripes)
            {
                pSync.uiBlackStripes.blackStripes_Lateral = pSync.lateralBlackStripes;
                pSync.uiBlackStripes.SetBlackStripesLateral();
            }

            EditorUtility.SetDirty(pSync.camSync);
        }

        //EditorGUI.BeginDisabledGroup(true);
        //EditorGUILayout.FloatField("Screen Ratio", pSync.screenRatio);
        //EditorGUI.EndDisabledGroup();

        //if (GUILayout.Button("Set camera"))
        //{
        //    pSync.SetCamera();
        //}
    }

    private void OnEnable()
    {
        pSync = (ProjectorSync)target;
        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        pSync.SetCamera();
    }
}