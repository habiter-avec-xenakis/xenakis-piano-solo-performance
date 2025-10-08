using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorRossler : StrangeAttractor
{
    public float a = 0.2f;
    public float b = 0.2f;
    public float c = 5.7f;
    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * -(position.y + position.z);
        y = position.y + t * (position.x + a * position.y);
        z = position.z + t * (b + position.z * (position.x - c));
    }
}