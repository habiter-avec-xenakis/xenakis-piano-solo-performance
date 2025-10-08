using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalTextureTargetRenderers : MusicalTextureTarget
{
    public Renderer rend;
    public string parameterName = "_BaseColor";
    protected override void Start()
    {
        base.Start();

        rend = GetComponent<Renderer>();

        mtManager.onSingleColorUpdate.AddListener(RenderersUpdate);
        mtManager.onDualColorUpdate.AddListener(OnColorUpdateDual);
    }

    void OnColorUpdateDual(Color colorMain, Color colorSecondary)
    {
        switch (targetMode)
        {
            case (TargetMode.Main):
                RenderersUpdate(colorMain);
                break;
            case (TargetMode.Secondary):
                RenderersUpdate(colorSecondary);
                break;
            case (TargetMode.Blend):
                RenderersUpdate(Color.Lerp(colorMain, colorSecondary, 0.5f));
                break;
        }
    }

    private void RenderersUpdate(Color color)
    {
        foreach (Material m in rend.materials)
        {
            m.SetColor(parameterName, color);
        }
    }
}
