using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ControlVFXAvatarWater : MonoBehaviour
{
    public VisualEffect vfx;

    public void EnableVFX(bool value)
    {
        vfx.enabled = value;
    }

    public void SetTurbulenceIntensity(float value)
    {
        vfx.SetFloat("TurbulenceIntensity", value);
    }
}