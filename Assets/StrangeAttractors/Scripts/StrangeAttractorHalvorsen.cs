using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorHalvorsen : StrangeAttractor
{
    public float a = 1.89f;
    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (-a * position.x - 4 * position.y - 4 * position.z - Mathf.Pow(position.y,2f));
        y = position.y + t * (-a * position.y - 4 * position.z - 4 * position.x - Mathf.Pow(position.z, 2f));
        z = position.z + t * (-a * position.z - 4 * position.x - 4 * position.y - Mathf.Pow(position.x, 2f));
    }
}