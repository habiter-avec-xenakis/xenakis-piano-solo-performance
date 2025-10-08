using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoudnessFresnel : AudioSourceLoudness
{
    [Header("Renderer")]
    public Renderer rend;

    public override void Update()
    {
        base.Update();
        float loudness = Mathf.Clamp(clipLoudness, 0f, loudnessCap);
        loudness = loudness * (1f / loudnessCap);

        rend.material.SetFloat("_mtFresnel", 5f + loudness * 5f);
    }
}
