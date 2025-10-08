using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXControlStrangeAttractors : MonoBehaviour
{
    public VisualEffect strangeAttractorsVFX;
    public float speed = 1f;
    public float scale = 1f;
    private Vector2 offset;

    private void Start()
    {
        offset = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f));
    }

    private void Update()
    {
        if(!strangeAttractorsVFX)
        {
            return;
        }

        strangeAttractorsVFX.SetFloat("Blend Lorenz", GetRandomValue(0f));
        strangeAttractorsVFX.SetFloat("Blend Dadras", GetRandomValue(4.5f));
        strangeAttractorsVFX.SetFloat("Blend Thomas", GetRandomValue(1.23f));
        strangeAttractorsVFX.SetFloat("Blend Chen", GetRandomValue(3.14f));
        strangeAttractorsVFX.SetFloat("Blend Aizawa", GetRandomValue(8.72f));
    }

    private float GetRandomValue(float y)
    {
        float value = scale * Mathf.PerlinNoise(Time.time * speed + offset.x, y + offset.y) + 0.5f - (scale / 2f);
        value = Mathf.Clamp01(value);

        return value;
    }
}