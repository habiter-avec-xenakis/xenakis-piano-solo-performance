using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToVisualsOffset : MonoBehaviour
{
    public DistanceToVisuals distanceToVisuals;
    public Transform additionalTransform;
    public Vector3 offset;

    private void Update()
    {
        transform.localPosition = offset + additionalTransform.localPosition - distanceToVisuals.handsCenter;
    }
}
