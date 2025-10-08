using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class VectorSpriteToPointCache : EditorWindow
{
    Sprite vectorSprite;
    string assetName = "New Point Cache";

    [MenuItem("Window/Sprite to PointCache")]
    static void Init()
    {
        VectorSpriteToPointCache window = (VectorSpriteToPointCache)EditorWindow.GetWindow(typeof(VectorSpriteToPointCache));
        window.Show();
    }

    void OnGUI()
    {
        if(vectorSprite && assetName == "New Point Cache")
        {
            assetName = vectorSprite.name.Replace("Sprite", "");
        }

        EditorGUILayout.LabelField("Sprite");
        EditorGUI.BeginChangeCheck();
        vectorSprite = EditorGUILayout.ObjectField(vectorSprite, typeof(Sprite), true) as Sprite;
        if (EditorGUI.EndChangeCheck() && vectorSprite && assetName == "New Point Cache")
        {
            assetName = vectorSprite.name.Replace("Sprite", "");
        }
        EditorGUILayout.LabelField("Asset Name");
        assetName = EditorGUILayout.TextField(assetName);

        if(GUILayout.Button("Bake"))
        {
            if (vectorSprite)
            {
                BakePointCache(vectorSprite.vertices);
            }
            else
            {
                Debug.LogWarning("Sprite field is empty.");
            }
        }
    }

    private void BakePointCache(Vector2[] vertices)
    {
        List<string> lines = new List<string>();

        lines.Add("pcache");
        lines.Add("format ascii 1.0");
        lines.Add("elements " + vertices.Length);
        lines.Add("property float position.x");
        lines.Add("property float position.y");
        lines.Add("property float position.z");
        lines.Add("property float normal.x");
        lines.Add("property float normal.y");
        lines.Add("property float normal.z");
        lines.Add("end_header");
        foreach(var v2 in vertices)
        {
            lines.Add(v2.x.ToString().Replace(',','.') + " " + v2.y.ToString().Replace(',', '.') + " " + "0 0 0 -1");
        }

        string filePath = Application.dataPath + "/PointCaches/" + assetName + ".pcache";

        StreamWriter sw = new StreamWriter(filePath);
        foreach(var line in lines)
        {
            sw.WriteLine(line);
        }
        sw.Close();

        AssetDatabase.Refresh();
    }
}