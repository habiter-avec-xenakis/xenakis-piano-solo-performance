using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.HighDefinition;

public class AudioSourceLoudnessCausticsDecal : AudioSourceLoudness
{
    //[Header("Renderers")]
    //public DecalProjector decalProjector;

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
        Debug.Log(loudness);

        //if(enableCaustics1)
        //{
        //    decalProjector.material.SetVector("Vector4_CC4FC01C", Vector4.Lerp(caustics1, targetCaustics1, clipLoudness));
        //}
        //if(enableCaustics2)
        //{
        //    decalProjector.material.SetVector("Vector4_21217F10", Vector4.Lerp(caustics2, targetCaustics2, clipLoudness));
        //}
        //if(enableSpeed1)
        //{
        //    decalProjector.material.SetVector("Vector2_54B2432F", Vector2.Lerp(speed1, targetSpeed1, clipLoudness));
        //}
        //if(enableSpeed2)
        //{
        //    decalProjector.material.SetVector("Vector2_3717E92A", Vector2.Lerp(speed2, targetSpeed2, clipLoudness));
        //}
        //if(enableOffset)
        //{
        //    decalProjector.material.SetFloat("Vector1_52F14B05", Mathf.Lerp(offset, targetOffset, clipLoudness));
        //}
        //if (enableColor)
        //{
        //    decalProjector.material.SetColor("Color_71F7EB5D", Color.Lerp(color, targetColor, clipLoudness));
        //}

    }
}
