using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorSprott : StrangeAttractor
{
    public float a = 2.07f;
    public float b = 1.79f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (position.y + a * position.x * position.y + position.x * position.z);
        y = position.y + t * (1 - b * Mathf.Pow(position.x, 2f) + position.y * position.z);
        z = position.z + t * (position.x - Mathf.Pow(position.x, 2f) - Mathf.Pow(position.y, 2f));
    } 
}