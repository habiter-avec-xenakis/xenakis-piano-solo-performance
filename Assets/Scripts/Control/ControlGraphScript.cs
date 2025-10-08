using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GraphScript))]
public class ControlGraphScript : MonoBehaviour
{
    private GraphScript graphScript;
    public ControlGraphScriptGroup graphScriptGroup;

    [Range(0f, 1f)]
    public float fade = 1f;

    //[Range(0f, 1f)]
    //public float scale = 0f;
    //public float minScale = 0.05f;
    //public float maxScale = 0.25f;

    //[Range(0f, 1f)]
    //public float width = 0f;
    //public float minWidth = 0f;
    //public float maxWidth = 0.5f;

    private void Awake()
    {
        graphScript = GetComponent<GraphScript>();
    }

    private void Update()
    {
        float fadeCurrent = fade;
        //float scaleCurrent = minScale + ((maxScale - minScale) * scale);
        //float widthCurrent = minWidth + ((maxWidth - minWidth) * width);

        if (graphScriptGroup != null)
        {
            fadeCurrent *= graphScriptGroup.fade;
            //scaleCurrent *= motionViewGroup.scale;
            //widthCurrent *= motionViewGroup.width;
        }

        //graphScript.GizmoAccScale = scaleCurrent;
        //graphScript.GizmoJerkScale = scaleCurrent;
        //graphScript.GizmoQuatScale = scaleCurrent;
        //graphScript.GizmoSpeedScale = scaleCurrent;

        //graphScript.TrailLinesWidth = widthCurrent;
        //graphScript.VectLinesWidth = widthCurrent;

        UpdateColorsAlpha(fadeCurrent);
    }

    void UpdateColorsAlpha(float alpha)
    {
        foreach(LineRenderer lr in graphScript.lineRenderersTrails)
        {
            lr.material.SetFloat("_Opacity", alpha);
        }
        foreach (LineRenderer lr in graphScript.lineRenderersVectors)
        {
            lr.material.SetFloat("_Opacity", alpha);
        }
    }
    public void SetFade(float value)
    {
        fade = value;
    }

    //public void SetScale(float value)
    //{
    //    scale = value;
    //}

    //public void SetWidth(float value)
    //{
    //    width = value;
    //}
}
