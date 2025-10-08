using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorChen : StrangeAttractor
{
    public float a = 5f;
    public float b = -10f;
    public float c = -0.38f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (a * position.x - position.y * position.z);
        y = position.y + t * (b * position.y + position.x * position.z);
        z = position.z + t * (c * position.z + (position.x * position.y)/3f);
    }
}