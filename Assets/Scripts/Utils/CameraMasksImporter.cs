using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraMasksImporter : MonoBehaviour
{
    private string[] maskNames = { "Front", "Ground", "Left", "Right" };

    private void Awake()
    {
        StartCoroutine(ImportMasks());
    }

    IEnumerator ImportMasks()
    {
        for (int i = 0; i < 4; i++)
        {
            var fileName = FileName(maskNames[i]);
            if (File.Exists(fileName))
            {
                Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
                WWW www = new WWW(fileName);
                yield return www;
                www.LoadImageIntoTexture(tex);
                PerformanceGlobalManager.cameraMasks[i] = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            }

            //Debug.Log("Mask_" + maskNames[i] + " | " + PerformanceGlobalManager.cameraMasks[i]);
        }
    }

    private string FileName(string suffix)
    {
        return Application.persistentDataPath + "/Masks/Mask_" + suffix + ".png";
    }
}