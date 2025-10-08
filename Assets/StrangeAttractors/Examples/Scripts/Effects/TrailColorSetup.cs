using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailColorSetup : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    public Renderer colorRend;

    private void Awake()
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        trailRenderer = GetComponent<TrailRenderer>();
        Material mat = trailRenderer.material;
        mat.SetFloat("ratio", ratio);
        if(colorRend)
        {
            trailRenderer.colorGradient = GradientFromColor(colorRend.material.GetColor("_EmissiveColor"));
        }
    }

    private Gradient GradientFromColor(Color color)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
