using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionViewToLightIntensity : MonoBehaviour
{
    public MotionView motionView;
    public Light lgt;
    private float intensityOriginal;

    private void Start()
    {
        intensityOriginal = lgt.intensity;
    }

    void Update()
    {
        lgt.intensity = intensityOriginal * motionView.CurrentSpeedMag;
    }
}
