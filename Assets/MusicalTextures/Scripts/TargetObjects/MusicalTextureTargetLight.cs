using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class MusicalTextureTargetLight : MusicalTextureTarget
{
    private Light m_light;
    protected override void Start()
    {
        base.Start();
        m_light = GetComponent<Light>();

        mtManager.onSingleColorUpdate.AddListener(LightUpdate);
        mtManager.onDualColorUpdate.AddListener(OnColorUpdateDual);
    }

    void OnColorUpdateDual(Color colorMain, Color colorSecondary)
    {
        switch(targetMode)
        {
            case (TargetMode.Main):
                LightUpdate(colorMain);
                break;
            case (TargetMode.Secondary):
                LightUpdate(colorSecondary);
                break;
            case (TargetMode.Blend):
                LightUpdate(Color.Lerp(colorMain, colorSecondary, 0.5f));
                break;
        }
    }

    private void LightUpdate(Color color)
    {
        m_light.color = color;
    }
}