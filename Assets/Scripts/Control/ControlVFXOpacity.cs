using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class ControlVFXOpacity : MonoBehaviour
{
    private VisualEffect vfx;

    private void Awake()
    {
        vfx = GetComponent<VisualEffect>();    
    }

    public void SetOpacity(float value)
    {
        vfx.SetFloat("Opacity", value);
    }
}
