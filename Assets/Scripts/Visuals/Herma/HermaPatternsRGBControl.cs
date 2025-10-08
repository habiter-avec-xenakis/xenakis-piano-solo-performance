using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermaPatternsRGBControl : MonoBehaviour
{
    public Renderer[] renderers;

    private void SetFloat(string name, float value)
    {
        foreach(var r in renderers)
        {
            r.material.SetFloat(name, value);
        }
    }

    public void SetOpacityR(float value)
    {
        SetFloat("_OpacityR", value);
    }

    public void SetOpacityG(float value)
    {
        SetFloat("_OpacityG", value);
    }
    public void SetOpacityB(float value)
    {
        SetFloat("_OpacityB", value);
    }

    public void SetFade(float value)
    {
        SetFloat("_Fade", value);
    }
}
