using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MusicalTextureScriptableObject))]
public class MusicalTextureEditor : Editor
{
    MusicalTextureScriptableObject mtScriptable;

    public void OnEnable()
    {
        mtScriptable = (MusicalTextureScriptableObject)target;
    }

    public override void OnInspectorGUI()
    {
        // Musical textures parsing
        int deleteIndex = -1;

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < mtScriptable.musicalTextures.Count; i++)
        {
            if(i > 0)
            {
                GUILayout.Space(20);
            }

            MusicalTexture mt = mtScriptable.musicalTextures[i];

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Musical texture " + (i + 1), EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("X", CustomEditorStyles.ButtonDelete()))
            {
                deleteIndex = i;
            }
            EditorGUILayout.EndHorizontal();

            mt.textureName = EditorGUILayout.TextField("Name", mt.textureName);
            mt.color = EditorGUILayout.ColorField("Color", mt.color);
            mt.intensity = EditorGUILayout.Slider("Intensity", mt.intensity, 0f, 2f);
            mt.roughness = EditorGUILayout.Slider("Roughness", mt.roughness, 0f, 1f);
            mt.chaos = EditorGUILayout.Slider("Chaos", mt.chaos, 0f, 1f);

            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(20);

        if(deleteIndex > -1)
        {
            mtScriptable.musicalTextures.RemoveAt(deleteIndex);
        }

        if (GUILayout.Button("New Musical Texture", CustomEditorStyles.ButtonNew()))
        {
            mtScriptable.musicalTextures.Add(new MusicalTexture());
        }

        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(mtScriptable);
        }


        //base.OnInspectorGUI();
    }
}
