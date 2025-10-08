using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoudnessCaustics : AudioSourceLoudness
{
    [Header("Renderers")]
    public Renderer[] renderers;

    [Header("Parameters")]
    public bool enableCaustics1 = false;
    public bool enableCaustics2 = false;
    public bool enableSpeed1 = false;
    public bool enableSpeed2 = false;
    public bool enableOffset = false;
    public bool enableColor = false;

    public Vector4 caustics1 = new Vector4(1, 1, 0, 0);
    public Vector2 speed1;
    public Vector4 caustics2 = new Vector4(1, 1, 0, 0);
    public Vector2 speed2;
    public float offset;
    public Color color;

    public Vector4 targetCaustics1 = new Vector4(1, 1, 0, 0);
    public Vector2 targetSpeed1;
    public Vector4 targetCaustics2 = new Vector4(1, 1, 0, 0);
    public Vector2 targetSpeed2;
    public float targetOffset;
    public Color targetColor;

    public override void Update()
    {
        base.Update();
        float loudness = Mathf.Clamp(clipLoudness, 0f, loudnessCap);
        loudness = loudness * (1f / loudnessCap);
        //Debug.Log(loudness);

        foreach (var r in renderers)
        {
            foreach(var m in r.materials)
            {
                if(enableCaustics1)
                {
                    m.SetVector("Vector4_5F1A4D63", Vector4.Lerp(caustics1, targetCaustics1, clipLoudness));
                }
                if(enableCaustics2)
                {
                    m.SetVector("Vector4_95FD30C4", Vector4.Lerp(caustics2, targetCaustics2, clipLoudness));
                }
                if(enableSpeed1)
                {
                    m.SetVector("Vector2_3C69078D", Vector2.Lerp(speed1, targetSpeed1, clipLoudness));
                }
                if(enableSpeed2)
                {
                    m.SetVector("Vector2_D825014F", Vector2.Lerp(speed2, targetSpeed2, clipLoudness));
                }
                if(enableOffset)
                {
                    m.SetFloat("Vector1_A2A0FDEE", Mathf.Lerp(offset, targetOffset, clipLoudness));
                }
                if (enableColor)
                {
                    m.SetColor("Color_71F7EB5D", Color.Lerp(color, targetColor, clipLoudness));
                }
            }
        }
    }
}
