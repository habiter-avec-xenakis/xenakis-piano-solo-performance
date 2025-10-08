using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

[RequireComponent(typeof(VisualEffect))]
public class VFXControlLorenz : MonoBehaviour
{
    private VisualEffect vfx;

    public UnityEventFloat onSpeedChanged = new UnityEventFloat();
    public UnityEventFloat onSigmaChanged = new UnityEventFloat();
    public UnityEventFloat onRhoChanged = new UnityEventFloat();
    public UnityEventFloat onBetaChanged = new UnityEventFloat();
    public UnityEventGradient onGradientChanged = new UnityEventGradient();
    public UnityEventBool onGradientFixedChanged = new UnityEventBool();
    public UnityEventFloat onRainbowBlendChanged = new UnityEventFloat();
    public UnityEventFloat onRainbowSpeedChanged = new UnityEventFloat();


    // Color Gradient
    private float gradientHueLeft = 0f;
    private float gradientHueRight = 0.5f;
    private Gradient gradientCurrent;
    private bool gradientFixed;
    private float m_rainbowTime;
    private float rainbowSpeed = 0.005f;

    public float rainbowTime
    {
        get { return m_rainbowTime;  }
    }

    private void Start()
    {
        vfx = GetComponent<VisualEffect>();

        onSpeedChanged.Invoke(vfx.GetFloat("Speed"));
        onSigmaChanged.Invoke(vfx.GetFloat("Sigma"));
        onRhoChanged.Invoke(vfx.GetFloat("Rho"));
        onBetaChanged.Invoke(vfx.GetFloat("Beta"));
        onGradientChanged.Invoke(vfx.GetGradient("ColorGradient"));
        onRainbowBlendChanged.Invoke(vfx.GetFloat("RainbowBlend"));
        onRainbowSpeedChanged.Invoke(rainbowSpeed);
    }

    private void Update()
    {
        m_rainbowTime += Time.deltaTime * rainbowSpeed;
        m_rainbowTime = Mathf.Repeat(m_rainbowTime, 1f);
        vfx.SetFloat("RainbowTime", m_rainbowTime);
    }

    public void SetSpeed(float value)
    {
        float mappedValue = GetMapping(value, 0.01f, 1f);
        vfx.SetFloat("Speed", mappedValue);
        onSpeedChanged.Invoke(mappedValue);
    }

    public void SetValueSigma(float value)
    {
        float mappedValue = GetMapping(value, 0f, 50f);
        vfx.SetFloat("Sigma", mappedValue);
        onSigmaChanged.Invoke(mappedValue);
    }

    public void SetValueRho(float value)
    {
        float mappedValue = GetMapping(value, 8f, 64f);
        vfx.SetFloat("Rho", mappedValue);
        onRhoChanged.Invoke(mappedValue);
    }

    public void SetValueBeta(float value)
    {
        float mappedValue = GetMapping(value, 0f, 16.5f);
        vfx.SetFloat("Beta", mappedValue);
        onBetaChanged.Invoke(mappedValue);
    }

    public void SetGradientHueLeft(float value)
    {
        gradientHueLeft = value;
        GradientUpdate();
    }

    public void SetGradientHueRight(float value)
    {
        gradientHueRight = value;
        GradientUpdate();
    }

    public void SetGradientType(float value)
    {
        if(value > 0.5f)
        {
            gradientFixed = !gradientFixed;
        }
        onGradientFixedChanged.Invoke(gradientFixed);
        GradientUpdate();
    }

    public void SetRainbowBlend(float value)
    {
        vfx.SetFloat("RainbowBlend", value);
        onRainbowBlendChanged.Invoke(value);
    }

    public void SetRainbowSpeed(float value)
    {
        float mappedValue = GetMapping(value, 0.005f, 1f);
        rainbowSpeed = mappedValue;
        onRainbowSpeedChanged.Invoke(mappedValue);
    }

    void GradientUpdate()
    {
        gradientCurrent = GradientFromHues(gradientHueLeft, gradientHueRight, gradientFixed);
        vfx.SetGradient("ColorGradient", gradientCurrent);
        onGradientChanged.Invoke(gradientCurrent);
    }

    Gradient GradientFromHues(float hueLeft, float hueRight, bool gradientFixed)
    {
        var gradient = new Gradient();

        if(gradientFixed)
        {
            gradient.mode = GradientMode.Fixed;
        }

        var colorKeys = new GradientColorKey[2];
        colorKeys[0].color = Color.HSVToRGB(hueLeft, 0.9f, 0.9f);
        if(gradientFixed)
        {
            colorKeys[0].time = 0.5f;
        }
        else
        {
            colorKeys[0].time = 0f;
        }

        colorKeys[1].color = Color.HSVToRGB(hueRight, 0.9f, 0.9f);
        colorKeys[1].time = 1f;

        var alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f;
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[1].time = 1f;

        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    private float GetMapping(float value, float min, float max)
    {
        float delta = max - min;
        return (min + value * delta);
    }
}
