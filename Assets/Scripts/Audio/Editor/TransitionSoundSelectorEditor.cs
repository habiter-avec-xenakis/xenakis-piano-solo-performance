using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransitionSoundSelector))]
public class TransitionSoundSelectorEditor : Editor
{
    private TransitionSoundSelector transitionSoundSelector;
    private string[] filesList;
    private int[] indices;

    private void OnEnable()
    {
        transitionSoundSelector = (TransitionSoundSelector)target;

        filesList = GetFileList();
        indices = GetIndices();
    }

    public override void OnInspectorGUI()
    {
        transitionSoundSelector.audioController = (AudioController)EditorGUILayout.ObjectField("Audio Controller", transitionSoundSelector.audioController, typeof(AudioController), true);

        EditorGUILayout.Space(20);


        for (int i = 0; i < transitionSoundSelector.transitionSounds.Length; i++)
        {
            EditorGUILayout.LabelField("Transition " + (i + 1), GUILayout.Width(80));

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();

            indices[i] = EditorGUILayout.Popup(indices[i], filesList);
            if (EditorGUI.EndChangeCheck())
            {
                transitionSoundSelector.transitionSounds[i].fileName = filesList[indices[i]];
                EditorUtility.SetDirty(transitionSoundSelector);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Volume", GUILayout.Width(60));
            //transitionSoundSelector.transitionSounds[i].volumeMultiplier = EditorGUILayout.FloatField(transitionSoundSelector.transitionSounds[i].volumeMultiplier);
            transitionSoundSelector.transitionSounds[i].volumeMultiplier = EditorGUILayout.Slider(transitionSoundSelector.transitionSounds[i].volumeMultiplier, 0f, 1f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);
        }

        EditorGUILayout.Space(20);
        
        if(GUILayout.Button("Refresh Files List"))
        {
            GetFileList();
            Repaint();
        }

        //base.OnInspectorGUI();
    }

    private string[] GetFileList()
    {
        string[] rawList = System.IO.Directory.GetFiles(Application.persistentDataPath + "/Sounds/");
        string[] list = new string[rawList.Length];

        for(int i = 0; i < list.Length; i++)
        {
            list[i] = Path.GetFileName(rawList[i]);
        }

        return list;
    }

    private int[] GetIndices()
    {
        int[] indices = new int[transitionSoundSelector.transitionSounds.Length];

        for(int i = 0; i < transitionSoundSelector.transitionSounds.Length; i++)
        {
            //Debug.Log(transitionSoundSelector.transitionSounds[i].fileName);

            for (int j = 0; j < filesList.Length; j++)
            {
                if (transitionSoundSelector.transitionSounds[i].fileName == filesList[j])
                {
                    indices[i] = j;
                }
            }
        }

        return indices;
    }
}