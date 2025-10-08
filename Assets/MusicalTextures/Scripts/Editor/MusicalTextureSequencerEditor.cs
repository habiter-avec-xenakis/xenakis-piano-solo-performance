using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

[CustomEditor(typeof(MusicalTextureSequencer))]
public class MusicalTextureSequencerEditor : Editor
{
    MusicalTextureSequencer mtSequencer;
    string[] mtNames;
    public void OnEnable()
    {
        mtSequencer = (MusicalTextureSequencer)target;

        if (mtSequencer.mtSeqIndexes == null || mtSequencer.mtSeqIndexes.Length == 0)
        {
            mtSequencer.mtSeqIndexes = new int[] { 0 };
            mtSequencer.mtSeqComments = new string[] { "" };
            mtSequencer.mtSeqSketches = new Texture2D[] { null };
        }

        if (mtSequencer.mtSeqSketches.Length != mtSequencer.mtSeqIndexes.Length)
        {
            mtSequencer.mtSeqSketches = new Texture2D[mtSequencer.mtSeqIndexes.Length];
        }

        // ! DIRTY !
        if(mtSequencer.mtManager == null)
        {
            mtSequencer.mtManager = mtSequencer.GetComponent<MusicalTextureManager>();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        GetMusicalTexturesNames();
        int removeIndex = -1;

        GUIStyle itemNumber = new GUIStyle();
        itemNumber.alignment = TextAnchor.MiddleCenter;
        itemNumber.fontSize = 25;
        itemNumber.normal.textColor = Color.white;

        EditorGUILayout.BeginVertical();

        mtSequencer.textCurrentSequence = (TextMeshProUGUI)EditorGUILayout.ObjectField("Text", mtSequencer.textCurrentSequence, typeof(TextMeshProUGUI), true);
        mtSequencer.sketchesRenderer = (Renderer)EditorGUILayout.ObjectField("Sketches renderer", mtSequencer.sketchesRenderer, typeof(Renderer), true);

        for (int i = 0; i < mtSequencer.mtSeqIndexes.Length; i++)
        {

            if (i != 0)
            {
                GUILayout.Space(10);
            }

            EditorGUILayout.BeginHorizontal();

            GUI.color = mtSequencer.mtManager.musicalTexturesData.musicalTextures[mtSequencer.mtSeqIndexes[i]].color;
            EditorGUILayout.LabelField((i).ToString(), itemNumber, GUILayout.Width(50), GUILayout.ExpandHeight(true));
            GUI.color = Color.white;

            EditorGUILayout.BeginVertical();
            mtSequencer.mtSeqIndexes[i] = EditorGUILayout.Popup(mtSequencer.mtSeqIndexes[i], mtNames);
            mtSequencer.mtSeqComments[i] = EditorGUILayout.TextField(mtSequencer.mtSeqComments[i]);
            mtSequencer.mtSeqSketches[i] = EditorGUILayout.ObjectField(mtSequencer.mtSeqSketches[i], typeof(Texture2D), true) as Texture2D;
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("X", CustomEditorStyles.ButtonDelete(), GUILayout.Width(20)))
            {
                removeIndex = i;
            }

            EditorGUILayout.EndHorizontal();
        }

        if(removeIndex > -1)
        {
            SequenceItemRemoveAt(removeIndex);
        }

        EditorGUILayout.EndVertical();

        GUILayout.Space(30);

        if (GUILayout.Button("Add sequence item", CustomEditorStyles.ButtonNew()))
        {
            SequenceItemCreate();
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    private void SequenceItemCreate()
    {
        List<int> indexes = ArrayToList(mtSequencer.mtSeqIndexes);
        List<string> comments = ArrayToList(mtSequencer.mtSeqComments);

        indexes.Add(new int());
        comments.Add("");

        mtSequencer.mtSeqIndexes = indexes.ToArray();
        mtSequencer.mtSeqComments = comments.ToArray();
    }

    private void SequenceItemRemoveAt(int index)
    {
        List<int> indexes = ArrayToList(mtSequencer.mtSeqIndexes);
        List<string> comments = ArrayToList(mtSequencer.mtSeqComments);

        indexes.RemoveAt(index);
        comments.RemoveAt(index);

        mtSequencer.mtSeqIndexes = indexes.ToArray();
        mtSequencer.mtSeqComments = comments.ToArray();
    }

    private void GetMusicalTexturesNames()
    {
        List<MusicalTexture> musicalTextures = mtSequencer.mtManager.musicalTexturesData.musicalTextures;
        mtNames = new string[musicalTextures.Count];
        for (int i = 0; i < musicalTextures.Count; i++)
        {
            mtNames[i] = musicalTextures[i].textureName;
        }
    }

    private List<string> ArrayToList(string[] strings)
    {
        List<string> stringsNew = new List<string>();

        foreach(string s in strings)
        {
            stringsNew.Add(s);
        }

        return stringsNew;
    }

    private List<int> ArrayToList(int[] ints)
    {
        List<int> intsNew = new List<int>();

        foreach (int i in ints)
        {
            intsNew.Add(i);
        }

        return intsNew;
    }
}
