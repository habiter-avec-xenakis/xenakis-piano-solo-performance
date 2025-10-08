using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Kino.PostProcessing;
using extOSC;

public class OSCMyosPostProcessControl : MonoBehaviour
{
    public OSCMyos oscMyos;
    public Volume volume;
    private Glitch glitch;
    private Streak streak;

    public float leftMin = 0f;
    public float leftMax = 0.75f;
    public float leftPow = 2f;

    public float rightMin = 0.1f;
    public float rightMax = 0.8f;
    public float rightPow = 2f;

    private void Start()
    {
        Glitch tmpGlitch;
        if (volume.profile.TryGet<Glitch>(out tmpGlitch))
        {
            glitch = tmpGlitch;
        }

        Streak tmpStreak;
        if (volume.profile.TryGet<Streak>(out tmpStreak))
        {
            streak = tmpStreak;
        }

        Debug.Log(glitch);
        Debug.Log(streak);
    }

    private void Update()
    {
        float lForceMeanPow = Mathf.Pow(oscMyos.lForceMean, leftPow);
        float rForceMeanPow = Mathf.Pow(oscMyos.rForceMean, rightPow);

        //glitch.block.value = MappedValue(oscMyos.lForceMean, 0f, 0.75f);
        //glitch.jitter.value = MappedValue(oscMyos.rForceMean, 0.1f, 0.8f);

        glitch.block.value = MappedValue(lForceMeanPow, leftMin, leftMax);
        glitch.jitter.value = MappedValue(rForceMeanPow, rightMin, rightMax);
    }

    private float MappedValue(float value, float valueMin, float valueMax)
    {
        return valueMin + (value * (valueMax - valueMin));
    }

    public void SetLeftMin(float value)
    {
        leftMin = value;
    }

    public void SetLeftMax(float value)
    {
        leftMax = value;
    }
    public void SetLeftPow(float value)
    {
        leftPow = value;
    }
    public void SetRighttMin(float value)
    {
        rightMin = value;
    }

    public void SetRightMax(float value)
    {
        rightMax = value;
    }
    public void SetRightPow(float value)
    {
        rightPow = value;
    }
}
