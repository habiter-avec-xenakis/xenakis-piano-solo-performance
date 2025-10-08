using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using extOSC;

[System.Serializable]
public class UnderwaterCaveVFXPreset
{
    public float caveWidth;
    public float caveHeight;
    //public float noiseBlend;
    public float noiseAmount;
    public float dotsSize;
    public UnityEvent onPresetApplied;
}

public class UnderwaterCaveVFXControl : MonoBehaviour
{
    [Header("Parameters")]
    public VisualEffect vfxCave;
    public VisualEffect vfxBubblesLeft;
    public VisualEffect vfxBubblesRight;
    public OSCMyos oscMyos;
    public UnderwaterCaveDiver diver;
    public UnderwaterCaveVFXPreset[] presets;
    [Range(0f,1f)]
    public float explode;

    [Header("Auto Mode")]
    public bool autoMode = false;
    public UnderwaterCaveDiver caveDiver;
    public float autoModeCaveSizeSpeed = 0.1f;
    public float autoModeDotsSizeSpeed = 0.025f;
    public float autoModeSpeed = 0.25f;

    public float autoModeBigNoiseSpeed = 0.01f;
    public float autoModeBigNoiseValue = 1f;
    public Vector2 autoModeBigNoise;
    public Vector2 autoModeBigNoiseOffset;

    [Header("Bubbles")]
    public float bubblesMultiplier;

    [Header("Starfield")]
    [Range(0f,1f)]
    public float starfieldAmount;
    public float starfieldExpansionSpeed = 0.1f;
    private float starfieldTimer = 0f;
    private bool starfieldExpand = false;
    public AnimationCurve starfieldDotsCurve;
    public AnimationCurve starfieldBlendCurve;

    private float timeStart;
    private float time;
    private float timeOffset;

    public bool debugSpheres;
    private GameObject debugOne;
    private GameObject debugTwo;

    private void Start()
    {
        if(vfxCave)
        {
            vfxCave.SetFloat("Noise Blend", 1f);
        }

        if(presets.Length >= 0)
        {
            ApplyPreset(0);
        }

        timeStart = Time.time;
        autoModeBigNoiseOffset = new Vector2(Mathf.PerlinNoise(timeStart, 0f) - 0.5f, Mathf.PerlinNoise(timeStart, 0.63f) - 0.5f) * autoModeBigNoiseValue * 2f;

        if(debugSpheres)
        {
            debugOne = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            debugOne.name = "Big Noise";
            debugTwo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            debugTwo.name = "Big Noise Offset";
            debugTwo.transform.localScale = new Vector3(1f, 1f, 0.1f);
        }
    }

    private void Update()
    {
        if(!autoMode)
        {
            return;
        }

        float caveWidth1 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed, 2f);
        float caveWidth2 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed * 3f, 1.23f);
        float caveWidth3 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed * 0.1f, 4.91f);
        float caveWidthBlend = Remap(caveWidth1 * caveWidth2 * caveWidth3, 1f, 12f) * 3f;

        float caveHeight1 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed + 1.3f, 3.27f);
        float caveHeight2 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed * 2.5f, 2.54f);
        float caveHeight3 = Mathf.PerlinNoise(Time.time * autoModeCaveSizeSpeed * 0.093f, 1.65f);
        float caveHeightBlend = Remap(caveHeight1 * caveHeight2 * caveHeight3, 1f, 12f) * 3f;

        vfxCave.SetFloat("Cave Width", caveWidthBlend + starfieldAmount * 15f);
        vfxCave.SetFloat("Cave Height", caveHeightBlend + starfieldAmount * 15f);

        float noiseAmount = Remap(Mathf.PerlinNoise(Time.time * autoModeSpeed + 2.5f, 3.27f), 0.1f, 0.25f + (explode * 2));
        vfxCave.SetFloat("Noise Amount", Mathf.Lerp(noiseAmount, 10f, starfieldAmount));

        float dotsSize = Remap(Mathf.PerlinNoise(Time.time * autoModeDotsSizeSpeed + 3.7f, 4.62f), 0.05f, 0.5f);
        vfxCave.SetFloat("Dots Size", Mathf.Lerp(dotsSize, 0.05f, starfieldDotsCurve.Evaluate(starfieldAmount)));

        time = timeStart + Time.deltaTime * autoModeBigNoiseSpeed;
        timeOffset = time - (50f * autoModeBigNoiseSpeed);

        autoModeBigNoise = new Vector2(Mathf.PerlinNoise(time, 0f) - 0.5f, Mathf.PerlinNoise(time, 0.63f) - 0.5f) * autoModeBigNoiseValue * 2f;
        autoModeBigNoiseOffset = new Vector2(Mathf.PerlinNoise(timeOffset, 0f) - 0.5f, Mathf.PerlinNoise(timeOffset, 0.63f) - 0.5f) * autoModeBigNoiseValue * 2f;

        caveDiver.positionOffset = autoModeBigNoiseOffset;

        vfxCave.SetVector2("Big Noise", autoModeBigNoise);

        vfxCave.SetFloat("Micro Noise Left", oscMyos.lForceMean);
        vfxCave.SetFloat("Micro Noise Right", oscMyos.rForceMean);

        vfxBubblesLeft.SetFloat("Amount", /*oscMyos.lForceMean * */diver.breathNormalized * bubblesMultiplier);
        vfxBubblesRight.SetFloat("Amount", /*oscMyos.rForceMean * */diver.breathNormalized * bubblesMultiplier);

        if(starfieldExpand)
        {
            starfieldAmount += Time.deltaTime * starfieldExpansionSpeed;
            vfxCave.SetFloat("Color Blend", starfieldBlendCurve.Evaluate(starfieldAmount));
            vfxCave.SetInt("Burst Count", (int)(256 + (1f - starfieldDotsCurve.Evaluate(starfieldAmount)) * 1792f));
        }

        if (debugSpheres)
        {
            debugOne.transform.position = autoModeBigNoise;
            debugTwo.transform.position = autoModeBigNoiseOffset;
        }
    }

    private float Remap(float value, float min, float max)
    {
        return (value * (max - min)) + min;
    }

    public void ApplyPreset(int index)
    {
        if(autoMode)
        {
            return;
        }

        index = Mathf.Clamp(index, 0, presets.Length - 1);

        var preset = presets[index];

        vfxCave.SetFloat("Cave Width", preset.caveWidth);
        vfxCave.SetFloat("Cave Height", preset.caveHeight);
        vfxCave.SetFloat("Noise Amount", preset.noiseAmount);
        vfxCave.SetFloat("Dots Size", preset.dotsSize);

        preset.onPresetApplied.Invoke();
    }

    public void SetCaveWidth(float value)
    {
        float width = (value * 4f) + 1;
        vfxCave.SetFloat("Cave Width", width);
    }

    public void SetCaveHeight(float value)
    {
        float height = (value * 4f) + 1;
        vfxCave.SetFloat("Cave Height", height);
    }

    public void SetBurstDelay(float value)
    {
        float delay = (value * 4.5f) + 0.5f;
        vfxCave.SetFloat("Burst Delay", value);
    }

    public void SetBurstCount(float value)
    {
        vfxCave.SetInt("Burst Count", (int)value);
    }

    public void SetNoiseBlend(float value)
    {
        vfxCave.SetFloat("Noise Blend", value);
    }

    public void SetNoiseAmount(float value)
    {
        vfxCave.SetFloat("Noise Amount", value);
    }

    public void SetMicroNoiseAmount(float value)
    {
        vfxCave.SetFloat("Micro Noise Amount", value);
    }

    public void SetMicroNoiseSpeed(float value)
    {
        vfxCave.SetFloat("Micro Noise Speed", value);
    }

    public void SetMicroNoiseIlluminationAmout(float value)
    {
        vfxCave.SetFloat("Micro Noise Illumination Amount", value);
    }

    public void SetMicroNoisePercentage(float value)
    {
        vfxCave.SetFloat("Micro Noise Percentage", value);
    }

    public void SetDotsSize(float value)
    {
        float dotsSize = (value * 0.95f) + 0.05f;
        vfxCave.SetFloat("Dots Size", dotsSize);
    }

    public void SetSpeed(float value)
    {
        float speed = value * 5;
        vfxCave.SetFloat("Speed", speed);
    }

    public void SetBubblesMultiplier(float value)
    {
        bubblesMultiplier = value;
    }

    public void BeginStarfieldExpansion()
    {
        starfieldExpand = true;
    }
}