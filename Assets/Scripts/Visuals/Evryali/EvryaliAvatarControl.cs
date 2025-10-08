using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EvryaliAvatarControl : MonoBehaviour
{
    public VisualEffect vfx;

    public void SetSpawnRatio(float value)
    {
        vfx.SetFloat("Spawn Ratio", value);
    }
    public void SetTurbulence(float value)
    {
        vfx.SetFloat("TurbulenceIntensity", value);
    }
}