using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;

[RequireComponent(typeof(Slider))]
public class MidiSlider : MonoBehaviour
{
    private Slider slider;
    public int targetKnobNumber;
    public bool smoothValue = true;
    public bool useGlobalValue = true;
    private bool smoothing = false;
    public float smoothTime = 0.3f;
    private float smoothVelocity;

    private float lastValue;
    private float targetValue;    

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if(useGlobalValue)
        {
            smoothTime = PerformanceGlobalManager.midiSlidersSmoothTime;
        }
    }

    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        if(knobNumber == targetKnobNumber)
        {
            float value = slider.minValue + knobValue * (slider.maxValue - slider.minValue);

            if (smoothValue)
            {
                targetValue = value;
                smoothing = true;
                lastValue = value;
            }
            else
            {
                slider.value = slider.minValue + knobValue * (slider.maxValue - slider.minValue);
            }
        }
    }

    private void Update()
    {
        if(smoothValue && smoothing)
        {
            slider.value = Mathf.SmoothDamp(slider.value, targetValue, ref smoothVelocity, smoothTime);
            lastValue = slider.value;

            //Debug.Log(smoothVelocity);
            if (Mathf.Abs(smoothVelocity) < 0.01f)
            {
                smoothing = false;
            }
        }
    }

    void OnEnable()
    {
        MidiMaster.knobDelegate += Knob;
    }

    void OnDisable()
    {
        MidiMaster.knobDelegate -= Knob;
    }
}
