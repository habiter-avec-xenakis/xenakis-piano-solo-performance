using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGraphScriptGroup : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fade;
    //[Range(0f, 1f)]
    //public float scale = 1f;
    //[Range(0f, 1f)]
    //public float width = 1f;

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
