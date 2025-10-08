using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenRatioShader : MonoBehaviour
{
    public string propertyName = "_ScreenRatio";
    public Renderer targetRenderer;

    private void Start()
    {
        if(targetRenderer)
        {
            targetRenderer.material.SetFloat(propertyName, (float)Screen.width / (float)Screen.height);
            Debug.Log("TOTO" + (float)Screen.width / (float)Screen.height);
        }
    }
}