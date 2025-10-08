using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Kino.PostProcessing;

[ExecuteInEditMode]
[RequireComponent(typeof(Volume))]
public class TransitionEffectsControl : MonoBehaviour
{
    private Volume _volume;

    private Glitch glitchComponent;
    [Header("Glitch")]
    [Range(0f,1f)]
    public float glitchBlock;
    [Range(0f, 1f)]
    public float glitchDrift;
    [Range(0f, 1f)]
    public float glitchJitter;
    [Range(0f, 1f)]
    public float glitchJump;
    [Range(0f, 1f)]
    public float glitchShake;

    private Streak streakComponent;
    [Header("Streak")]
    [Range(0f, 1f)]
    public float streakThreshold;
    [Range(0f, 1f)]
    public float streakStrech;
    [Range(0f, 1f)]
    public float streakIntensity;
    public Color streakTint;

    private Utility utilityComponent;
    [Header("Utility")]
    [Range(0f, 1f)]
    public float utilitySaturation;
    [Range(0f, 1f)]
    public float utilityHueShift;
    [Range(0f, 1f)]
    public float utilityInvert;
    public Color utilityFade;

    private void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out glitchComponent);
        _volume.profile.TryGet(out streakComponent);
        _volume.profile.TryGet(out utilityComponent);

        // Glitch
        glitchBlock = glitchComponent.block.GetValue<float>();
        glitchDrift = glitchComponent.drift.GetValue<float>();
        glitchJitter = glitchComponent.jitter.GetValue<float>();
        glitchJump = glitchComponent.jump.GetValue<float>();
        glitchShake = glitchComponent.shake.GetValue<float>();

        // Streak
        streakThreshold = streakComponent.threshold.GetValue<float>();
        streakStrech = streakComponent.stretch.GetValue<float>();
        streakIntensity = streakComponent.intensity.GetValue<float>();
        streakTint = streakComponent.tint.GetValue<Color>();

        // Utility
        utilitySaturation = utilityComponent.saturation.GetValue<float>();
        utilityHueShift = utilityComponent.hueShift.GetValue<float>();
        utilityInvert = utilityComponent.invert.GetValue<float>();
        utilityFade = utilityComponent.fade.GetValue<Color>();
    }

    private void Update()
    {
        // Glitch
        glitchComponent.block.SetValue(new FloatParameter(glitchBlock));
        glitchComponent.drift.SetValue(new FloatParameter(glitchDrift));
        glitchComponent.jitter.SetValue(new FloatParameter(glitchJitter));
        glitchComponent.jump.SetValue(new FloatParameter(glitchJump));
        glitchComponent.shake.SetValue(new FloatParameter(glitchShake));

        // Streak
        streakComponent.threshold.SetValue(new FloatParameter(streakThreshold));
        streakComponent.stretch.SetValue(new FloatParameter(streakStrech));
        streakComponent.intensity.SetValue(new FloatParameter(streakIntensity));
        streakComponent.tint.SetValue(new ColorParameter(streakTint));

        // Utility
        utilityComponent.saturation.SetValue(new FloatParameter(utilitySaturation));
        utilityComponent.hueShift.SetValue(new FloatParameter(utilityHueShift));
        utilityComponent.invert.SetValue(new FloatParameter(utilityInvert));
        utilityComponent.fade.SetValue(new ColorParameter(utilityFade));
    }
}