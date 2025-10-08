using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorThomas : StrangeAttractor
{
    public float b = 0.208186f;
    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (Mathf.Sin(position.y) - b * position.x);
        y = position.y + t * (Mathf.Sin(position.z) - b * position.y);
        z = position.z + t * (Mathf.Sin(position.x) - b * position.z);
    }
}