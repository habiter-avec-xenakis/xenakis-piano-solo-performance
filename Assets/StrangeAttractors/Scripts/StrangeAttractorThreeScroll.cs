using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorThreeScroll : StrangeAttractor
{
    public float a = 32.48f;
    public float b = 45.84f;
    public float c = 1.18f;
    public float d = 0.13f;
    public float e = 0.57f;
    public float f = 14.7f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (a * (position.y - position.x) + d * position.x * position.z);
        y = position.y + t * (b * position.x - position.x * position.z + f * position.y);
        z = position.z + t * (c * position.z + position.x * position.y - e * Mathf.Pow(position.x, 2f));
    } 
}