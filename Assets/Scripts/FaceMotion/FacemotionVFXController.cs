using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FacemotionVFXController : MonoBehaviour
{
    public Transform rotationTransform;
    public VisualEffect vfx;

    [Range(0f,1f)]
    public float abstraction;
    [Range(0f,1f)]
    public float abstractionRadius;

    [Range(0f,1f)]
    public float snap;
    [Range(0f, 1f)]
    public float snapStep;

    [Range(0f, 1f)]
    public float colorR;
    [Range(0f, 1f)]
    public float colorG;
    [Range(0f, 1f)]
    public float colorB;

    [Range(0f, 1f)]
    public float randomRGB;

#if UNITY_EDITOR
    private void Update()
    {
        SetAbstraction(abstraction);
        SetSnap(snap);
        //SetSnapStep(snapStep);

        SetColorR(colorR);
        SetColorG(colorG);
        SetColorB(colorB);
    }
#endif

    public void SetAbstraction(float value)
    {
        abstraction = value;
        vfx.SetFloat("Abstraction", value);
    }

    public void SetAbstractionRadius(float value)
    {
        vfx.SetFloat("Sphere Radius", value * 3f);
    }

    public void SetSnap(float value)
    {
        snap = value;
        vfx.SetFloat("Snap", value);
    }

    public void SetSnapStep(float value)
    {
        int step = Mathf.RoundToInt((value * 95) + 5);
        vfx.SetInt("Snap Step", step);
    }

    public void SetColorR(float value)
    {
        colorR = value;
        vfx.SetFloat("Color R", value);
    }
    public void SetColorG(float value)
    {
        colorG = value;
        vfx.SetFloat("Color G", value);
    }
    public void SetColorB(float value)
    {
        colorB = value;
        vfx.SetFloat("Color B", value);
    }

    public void SetRandomRGB(float value)
    {
        randomRGB = value;
        vfx.SetFloat("Random RGB", value);
    }

    public void SetFade(float value)
    {
        vfx.SetFloat("Fade", value);
    }

    public void OnDetected()
    {
        //Debug.Log("Detected");
        vfx.SetFloat("Turbulence Amount", 0f);
    }

    public void OnDetectionLost()
    {
        //Debug.Log("Detection Lost");
        vfx.SetFloat("Turbulence Amount", 1f);
    }
}