using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCaustics : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fade;
    [Range(0f, 1f)]
    public float scale;

    public Renderer rend;
    private Vector4 caustics1;
    private Vector4 caustics2;

    private void Start()
    {
        caustics1 = rend.material.GetVector("Vector4_B8750039");
        caustics2 = rend.material.GetVector("Vector4_F8A90CB9");
    }

    void Update()
    {
        rend.material.SetFloat("Strength", fade);

        float causticsScale = scale + 1f;

        rend.material.SetVector("Vector4_B8750039", new Vector4(caustics1.x * causticsScale, caustics1.y * causticsScale, caustics1.y - causticsScale, caustics1.z - causticsScale));
        rend.material.SetVector("Vector4_F8A90CB9", new Vector4(caustics2.x * causticsScale, caustics2.y * causticsScale, caustics2.y - causticsScale, caustics2.z - causticsScale));
    }

    public void SetFade(float value)
    {
        fade = value;
    }

    public void SetScale(float value)
    {
        scale = value;
    }
}
