using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StrangeAttractorPointCacheCreator : EditorWindow
{
    public StrangeAttractorType saType;
    public int sampleStart = 0;
    public int sampleCount = 1000;
    public float sampleStep = 0.01f;
    private Vector3[] samples;
    private Vector3[] samplesOrientation;
    string assetSuffix = "Default";
    public string assetPath = "StrangeAttractors/Examples/PointCaches";

    [MenuItem("Window/Strange attractor PointCache creator")]
    static void Init()
    {
        StrangeAttractorPointCacheCreator window = (StrangeAttractorPointCacheCreator)EditorWindow.GetWindow(typeof(StrangeAttractorPointCacheCreator));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Sample type");
        saType = (StrangeAttractorType)EditorGUILayout.EnumPopup(saType);
        EditorGUILayout.LabelField("Samples");
        sampleStart = EditorGUILayout.IntField(sampleStart);
        sampleCount = EditorGUILayout.IntField(sampleCount);
        sampleStep = EditorGUILayout.FloatField(sampleStep);
        EditorGUILayout.LabelField("File");

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.TextField(assetPath, GUILayout.Width(250));
        EditorGUILayout.LabelField(saType.ToString());
        EditorGUILayout.LabelField("_");
        EditorGUILayout.TextField(assetSuffix);
        EditorGUILayout.LabelField(".pcache");
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Bake"))
        {
            GetSamples();
            BakePointCache(samples, samplesOrientation);
        }
    }

    private void GetSamples()
    {
        StrangeAttractor strangeAttractor = StrangeAttractorUtils.GetStrangeAttractorFromType(saType);

        Vector3 sample = new Vector3(0.1f, 0.1f, 0);
        Vector3 sampleOrientation = Vector3.zero;

        for(int i = 0; i < sampleStart; i++)
        {
            sample = strangeAttractor.GetNewPosition(sample, sampleStep);
        }

        samples = new Vector3[sampleCount];
        samplesOrientation = new Vector3[sampleCount];
        for (int j = 0; j < sampleCount; j++)
        {
            sample = strangeAttractor.GetNewPosition(sample, sampleStep);
            samples[j] = sample;

            sampleOrientation = strangeAttractor.rotation * Vector3.forward;
            samplesOrientation[j] = sampleOrientation;
        }
    }

    private void BakePointCache(Vector3[] vertices, Vector3[] normals)
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

        for (int i = 0; i < sampleCount; i++)
        {
            lines.Add(samples[i].x.ToString().Replace(',', '.') + " " + samples[i].y.ToString().Replace(',', '.') + " " + samples[i].z.ToString().Replace(',', '.') + " " + samplesOrientation[i].x.ToString().Replace(',', '.') + " " + samplesOrientation[i].y.ToString().Replace(',', '.') + " " + samplesOrientation[i].z.ToString().Replace(',', '.'));
        }

        string filePath = Application.dataPath + "/" + assetPath + "/" + saType.ToString() + "_" + assetSuffix + ".pcache";

        StreamWriter sw = new StreamWriter(filePath);
        foreach (var line in lines)
        {
            sw.WriteLine(line);
        }
        sw.Close();

        AssetDatabase.Refresh();
    }
}