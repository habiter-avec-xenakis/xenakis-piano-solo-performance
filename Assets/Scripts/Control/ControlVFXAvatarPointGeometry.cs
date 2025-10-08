using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ControlVFXAvatarPointGeometry : MonoBehaviour
{
    public VisualEffect vfx;

    public void SetBurstDelay(float value)
    {
        vfx.SetFloat("BurstDelay", value);
    }

    public void SetVelocityMultiplier(float value)
    {
        vfx.SetFloat("VelocityMultiplier", value);
    }
    
    public void SetVelocityScaleAmount(float value)
    {
        vfx.SetFloat("VelocityScaleAmount", value);
    }

    public void SetNoiseMultiplier(float value)
    {
        vfx.SetFloat("NoiseMultiplier", value);
    }

    public void SetConstantOpacity(float value)
    {
        vfx.SetFloat("Constant Opacity", value);
    }

    public void SetFixedOpacity(float value)
    {
        vfx.SetFloat("Fixed Opacity", value);
    }
}
