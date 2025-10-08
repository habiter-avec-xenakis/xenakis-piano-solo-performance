using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorLorenz83 : StrangeAttractor
{
    public float a = 0.95f;
    public float b = 7.91f;
    public float f = 4.83f;
    public float g = 4.66f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (-a * position.x - position.y * position.y - position.z * position.z + a * f);
        y = position.y + t * (-position.y + position.x * position.y - b * position.x * position.z + g);
        z = position.z + t * (-position.z + b * position.x * position.y + position.x * position.z);
    }
}