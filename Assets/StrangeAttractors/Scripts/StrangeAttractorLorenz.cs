using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorLorenz : StrangeAttractor
{
    public float a = 10.0f;
    public float b = 28.0f;
    public float c = 8.0f / 3.0f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * a * (position.y - position.x);
        y = position.y + t * (position.x * (b - position.z) - position.y);
        z = position.z + t * (position.x * position.y - c * position.z);
    }
}