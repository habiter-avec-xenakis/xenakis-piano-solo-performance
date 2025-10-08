using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ControlSliderFade : MonoBehaviour
{
    private Slider slider;

    public float fadeTime = 3f;
    public bool instant = false;

    private bool silence = false;
    private bool silenceLast = false;
    private float savedValue = 1f;

    public UnityEvent onFadingIn = new UnityEvent();
    public UnityEvent onFadingOut = new UnityEvent();

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void InstantOn()
    {
        slider.value = savedValue;
        slider.interactable = true;
    }

    private void InstantOff()
    {
        savedValue = slider.value;
        slider.value = 0f;
        slider.interactable = false;
    }

    public void FadeIn()
    {
        silence = false;
        if(silence == silenceLast)
        {
            return;
        }

        onFadingIn.Invoke();

        if(instant)
        {
            InstantOn();
        }
        else
        {
            StartCoroutine(SliderFade(fadeTime, true));
        }

        silenceLast = false;
    }

    public void FadeOut()
    {
        silence = true;
        if (silence == silenceLast)
        {
            return;
        }

        onFadingOut.Invoke();

        if (instant)
        {
            InstantOff();
        }
        else
        {
            savedValue = slider.value;
            StartCoroutine(SliderFade(fadeTime, false));
        }

        silenceLast = true;
    }

    IEnumerator SliderFade(float duration, bool fadeIn)
    {
        slider.interactable = false;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            if(fadeIn)
            {
                slider.value = elapsedTime / duration;
            }
            else
            {
                slider.value = 1f - (elapsedTime / duration);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(fadeIn)
        {
            slider.value = savedValue;
            slider.interactable = true;
        }
        else
        {
            slider.value = 0f;
            slider.interactable = false;
        }
    }
}
