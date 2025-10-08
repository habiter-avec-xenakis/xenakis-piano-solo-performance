using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

[RequireComponent(typeof(LeftRightFromHands))]
public class LeftRightFromHandsToVFX : MonoBehaviour
{
    private LeftRightFromHands lrfh;
    public VisualEffect vfx;
    public bool useSmoothedValue;

    public HDAdditionalLightData lightLeft;
    public HDAdditionalLightData lightRight;
    public float lightPosScale = 1f;
    [Range(0f, 0.75f)]
    public float lightIntensityMin;
    [Range(0f, 10f)]
    public float lightIntensityBoost = 0.5f;
    private float lightIntensityBase;

    private void Start()
    {
        lrfh = GetComponent<LeftRightFromHands>();

        if(lightLeft)
        {
            lightIntensityBase = lightLeft.intensity;
        }
    }

    private void Update()
    {
        if(vfx)
        {
            if(useSmoothedValue)
            {
                vfx.SetFloat("LeftRight", lrfh.normalizedAverageSmoothed);
            }
            else
            {
                vfx.SetFloat("LeftRight", lrfh.normalizedAverageRaw);
            }
        }

        if(lightLeft && lightRight)
        {
            float valueLeft;
            float valueRight;

            if(useSmoothedValue)
            {
                valueLeft = lrfh.normalizedLeftSmoothed;
                valueRight = lrfh.normalizedRightSmoothed;
            }
            else
            {
                valueLeft = lrfh.normalizedLeft;
                valueRight = lrfh.normalizedRight;
            }


            lightLeft.transform.localPosition = new Vector3(valueLeft, 0, 0) * -lightPosScale;
            lightRight.transform.localPosition = new Vector3(valueRight, 0, 0) * lightPosScale;

            lightLeft.intensity = lightIntensityBase * valueLeft * (lightIntensityMin + (1f - lightIntensityMin)) * (1f + lightIntensityBoost);
            lightRight.intensity = lightIntensityBase * valueRight * (lightIntensityMin + (1f - lightIntensityMin)) * (1f + lightIntensityBoost);
        }
    }
}
