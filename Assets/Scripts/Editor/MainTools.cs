using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MainTools : EditorWindow
{
    [MenuItem("Window/Main Tools")]
    static void Init()
    {
        MainTools window = (MainTools)EditorWindow.GetWindow(typeof(MainTools));
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Open Main Scene"))
        {
            EditorSceneManager.OpenScene("Assets/Scenes/System/System_Main.unity");
        }

        if (GUILayout.Button("Open Persistent Data Folder"))
        {
            ShowExplorer(Application.persistentDataPath);
        }

        if (GUILayout.Button("Get Webcam Names"))
        {
            foreach (var d in WebCamTexture.devices)
            {
                Debug.Log(d.name);
            }
        }
    }

    public void ShowExplorer(string itemPath)
    {
        itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
    }
}
