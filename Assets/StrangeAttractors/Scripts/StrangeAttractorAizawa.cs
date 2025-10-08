using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorAizawa : StrangeAttractor
{
    public float a = 0.95f;
    public float b = 0.7f;
    public float c = 0.6f;
    public float d = 3.5f;
    public float e = 0.25f;
    public float f = 0.1f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * ((position.z - b) * position.x - d * position.y);
        y = position.y + t * (d * position.x + (position.z - b) * position.y);
        z = position.z + t * (c + a * position.z - (Mathf.Pow(position.z, 3f) / 3f) - (Mathf.Pow(position.x, 2) + Mathf.Pow(position.y, 2)) * (1f + e * position.z) + f * position.z * Mathf.Pow(position.x, 3f));
    }
}