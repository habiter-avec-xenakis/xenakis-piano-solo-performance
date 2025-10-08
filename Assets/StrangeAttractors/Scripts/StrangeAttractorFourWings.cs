using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractorFourWings : StrangeAttractor
{
    public float a = 0.2f;
    public float b = 0.01f;
    public float c = -0.4f;

    protected override void CalculatePosition(Vector3 position, float t)
    {
        x = position.x + t * (a * position.x + position.y * position.z);
        y = position.y + t * (b * position.x + c * position.y - position.x * position.z);
        z = position.z + t * (-position.z - position.x * position.y);
    } 
}