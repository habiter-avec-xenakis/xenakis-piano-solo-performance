using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHermaControls : MonoBehaviour
{
    public HermaControls hermaControls;

    public void SetLasersLifetime(float value)
    {
        hermaControls.vfxLasers.SetFloat("Lifetime Base", value);
    }

    public void SetLasersMultiplier(float value)
    {
        hermaControls.vfxLasers.SetFloat("Multiplier", value);
    }
}