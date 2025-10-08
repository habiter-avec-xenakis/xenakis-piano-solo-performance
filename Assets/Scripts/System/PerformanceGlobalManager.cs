using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Kino.PostProcessing;
using System;
using System.IO;

public static class PerformanceGlobalManager
{
    //public static int controlsDisplay = 0;
    //public static UnityEventInt onControlsDisplayChanged = new UnityEventInt();
    public static UnityEvent onPreSceneChange = new UnityEvent();
    public static GameObject subSequencePanel;
    public static GameObject currentControlsPanel;
    public static int transitionIndex = 0;
    public static bool systemInUse = false;
    public static Utility volumeUtilityFade;
    public static Sprite[] cameraMasks = new Sprite[4];
    public static float midiSlidersSmoothTime = 0.2f;

    //private static string iniFilePath;

    //public static void OnControlsDisplayChanged(int index)
    //{
    //    Debug.Log("OnControlsDisplayChanged()");
    //    controlsDisplay = index;
    //}

    //public static void SetIniFilePath(string path)
    //{
    //    iniFilePath = path;
    //}

    public static void IniFileCheck()
    {
        if (!File.Exists(GetIniFilePath()))
        {
            StreamWriter sw = new StreamWriter(GetIniFilePath());
            sw.Close();
        }
    }

    public static float GetIniFileParameterFloat(string paramName)
    {
        float value = 0f;

        var lines = File.ReadAllLines(GetIniFilePath());

        foreach(var l in lines)
        {
            var s = l.Split(':');
            if(s[0] == paramName)
            {
                value = float.Parse(s[1]);
            }
        }

        return value;
    }

    public static string GetIniFileParameterString(string paramName)
    {
        string value = "";

        var lines = File.ReadAllLines(GetIniFilePath());

        foreach (var l in lines)
        {
            var s = l.Split(':');

            if (s[0] == paramName)
            {
                value = s[1].Trim();
            }
        }

        return value;
    }

    public static void SetIniFileParameter(string paramName, string value)
    {
        var lines = File.ReadAllLines(GetIniFilePath());

        int index = -1;
        for(int i = 0; i < lines.Length; i++)
        {
            string existingParam = lines[i].Split(':')[0].Trim();

            if (existingParam == paramName)
            {
                index = i;
            }
        }

        string appendText = paramName + ":" + value;
        if (index == -1)
        {
            File.AppendAllText(GetIniFilePath(), Environment.NewLine + appendText);
        }
        else
        {
            lines[index] = appendText;
            File.WriteAllLines(GetIniFilePath(), lines);
            Debug.Log("Parameter already exists, updating.");
        }
    }

    private static string GetIniFilePath()
    {
        return Application.persistentDataPath + "/Config.ini";
    }
}