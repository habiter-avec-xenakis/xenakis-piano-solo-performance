using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorDadras : StrangeAttractor
{
    public float a = 3.0f;
    public float b = 2.7f;
    public float c = 1.7f;
    public float d = 2.0f;
    public float e = 9.0f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (position.y - a * position.x + b * position.y * position.z);
        y = position.y + t * (c * position.y - position.x * position.z + position.z);
        z = position.z + t * (d * position.x * position.y - e * position.z);
    }
}