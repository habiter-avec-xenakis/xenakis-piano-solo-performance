using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BlendShapesChecker : EditorWindow  
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string nameExclude = "blendShape1.";
    public string textFilePath = "D:/Projects/Xenakis";

    private Vector2 scrollPos;
    private float maxWeight = 0f;

    [MenuItem("Window/Blend shapes checker")]
    static void Init()
    {
        BlendShapesChecker window = (BlendShapesChecker)GetWindow(typeof(BlendShapesChecker));
        window.Show();
    }

    private void OnGUI()
    {
        skinnedMeshRenderer = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Renderer", skinnedMeshRenderer, typeof(SkinnedMeshRenderer), true);

        if(!skinnedMeshRenderer)
        {
            EditorGUILayout.LabelField("No skinned mesh renderer selected.");
            return;
        }

        var mesh = skinnedMeshRenderer.sharedMesh;
        nameExclude = EditorGUILayout.TextField("Name exclude", nameExclude);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Text file", GUILayout.Width(52));
        textFilePath = EditorGUILayout.TextField(textFilePath);
        EditorGUILayout.LabelField("/" + skinnedMeshRenderer.name + "_BlendShapeNames.txt");
        if (GUILayout.Button("Write file"))
        {
            List<string> weightNames = new List<string>();
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                weightNames.Add(mesh.GetBlendShapeName(i));
            }
            WriteFile(textFilePath + "/" + skinnedMeshRenderer.name + "_BlendShapeNames.txt", weightNames);

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Maximum weight " + maxWeight);
        EditorGUILayout.BeginScrollView(scrollPos, false, false);

        for(int i = 0; i < mesh.blendShapeCount; i++)
        {
            var name = mesh.GetBlendShapeName(i).Replace(nameExclude, "");

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));
            EditorGUILayout.LabelField(i.ToString("00"), GUILayout.Width(20));
            EditorGUILayout.LabelField(name);

            var currentWeight = skinnedMeshRenderer.GetBlendShapeWeight(i);
            if(currentWeight > maxWeight)
            {
                maxWeight = currentWeight;
            }

            EditorGUILayout.LabelField(currentWeight.ToString("00.00"), GUILayout.Width(40));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        Repaint();
    }

    private void WriteFile(string path, List<string> names)
    {
        var writer = new StreamWriter(path);
        foreach(var name in names)
        {
            writer.WriteLine(name);
        }
        writer.Close();
    }
}
