using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MotionView))]
public class ControlMotionView : MonoBehaviour
{
    private MotionView motionView;
    public ControlMotionViewGroup motionViewGroup;

    [Range(0f, 1f)]
    public float fade = 1f;

    [Range(0f, 1f)]
    public float scale = 0f;
    public float minScale = 0.05f;
    public float maxScale = 0.25f;

    [Range(0f, 1f)]
    public float width = 0f;
    public float minWidth = 0f;
    public float maxWidth = 0.5f;

    private void Awake()
    {
        motionView = GetComponent<MotionView>();
    }

    private void Update()
    {
        float fadeCurrent = fade;
        float scaleCurrent = minScale + ((maxScale - minScale) * scale);
        float widthCurrent = minWidth + ((maxWidth - minWidth) * width);

        if (motionViewGroup != null)
        {
            fadeCurrent *= motionViewGroup.fade;
            scaleCurrent *= motionViewGroup.scale;
            widthCurrent *= motionViewGroup.width;
        }

        motionView.GizmoAccScale = scaleCurrent;
        motionView.GizmoJerkScale = scaleCurrent;
        motionView.GizmoQuatScale = scaleCurrent;
        motionView.GizmoSpeedScale = scaleCurrent;

        motionView.TrailLinesWidth = widthCurrent;
        motionView.VectLinesWidth = widthCurrent;

        UpdateColorsAlpha(fadeCurrent);
    }

    void UpdateColorsAlpha(float alpha)
    {
        foreach(LineRenderer lr in motionView.lineRenderersTrails)
        {
            lr.material.SetFloat("_Opacity", alpha);
        }
        foreach (LineRenderer lr in motionView.lineRenderersVectors)
        {
            lr.material.SetFloat("_Opacity", alpha);
        }
    }
    public void SetFade(float value)
    {
        fade = value;
    }

    public void SetScale(float value)
    {
        scale = value;
    }

    public void SetWidth(float value)
    {
        width = value;
    }
}
