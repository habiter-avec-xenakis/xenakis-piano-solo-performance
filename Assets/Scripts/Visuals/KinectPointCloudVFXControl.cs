using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KinectPointCloudVFXControl : MonoBehaviour
{
    public VisualEffect kinectVfx;

    public void SetOpacity(float value)
    {
        kinectVfx.SetFloat("Opacity", value);
    }

    public void SetSize(float value)
    {
        kinectVfx.SetFloat("Size", value);
    }
}