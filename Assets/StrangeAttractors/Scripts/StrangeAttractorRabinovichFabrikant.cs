using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorRabinovichFabrikant : StrangeAttractor
{
    public float a = 0.14f;
    public float b = 0.1f;
    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (position.y * (position.z - 1f + Mathf.Pow(position.x, 2f)) + b * position.x);
        y = position.y + t * (position.x * (3f * position.z + 1f - Mathf.Pow(position.x, 2f)) + b * position.y);
        z = position.z + t * (-2f * position.z * (a + position.x * position.y));
    }
}