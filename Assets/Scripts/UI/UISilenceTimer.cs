using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SilenceCounterState { Idle, Counting, CountingOvertime }

public class UISilenceTimer : MonoBehaviour
{
    private MusicalTextureTrackSequencer mtTrackSequencer;
    public SilenceCounterState silenceCounterState = SilenceCounterState.Idle;

    public Color colorNormal = Color.blue;
    public Color colorOvertime = Color.red;

    public Image imageBackground;
    public Image imageFrame;
    public TextMeshProUGUI textCounter;

    private float counter = 0f;
    private float ratio;
    private float currentDuration;

    private void Awake()
    {
        imageBackground.color = colorNormal;
        textCounter.color = Color.grey;
        imageFrame.color = Color.grey;

        mtTrackSequencer = FindObjectOfType<MusicalTextureTrackSequencer>();
        if(mtTrackSequencer != null)
        {
            mtTrackSequencer.onSilenceBegin.AddListener(OnSilenceBegin);
            mtTrackSequencer.onSilenceEnd.AddListener(OnSilenceEnd);
        }
    }

    private void Update()
    {
        if(silenceCounterState != SilenceCounterState.Idle)
        {
            counter += Time.deltaTime;
            textCounter.text = counter.ToString("00.0");
            imageBackground.fillAmount = counter * ratio;
        }

        switch(silenceCounterState)
        {
            case (SilenceCounterState.Idle):
                break;

            case (SilenceCounterState.Counting):

                if(counter >= currentDuration)
                {
                    imageBackground.color = colorOvertime;
                    silenceCounterState = SilenceCounterState.CountingOvertime;
                }
                break;

            case (SilenceCounterState.CountingOvertime):
                if(counter > 99f)
                {
                    textCounter.text = "99+";
                    silenceCounterState = SilenceCounterState.Idle;
                }
                break;
        }
    }

    private void OnSilenceBegin(float duration)
    {
        //Debug.Log("OnSilenceBegin(" + duration + ")");

        ratio = 1f / duration;

        counter = 0f;
        currentDuration = duration;
        imageBackground.fillAmount = 0f;

        imageBackground.color = colorNormal;
        textCounter.color = Color.white;
        imageFrame.color = Color.white;

        silenceCounterState = SilenceCounterState.Counting;
    }

    private void OnSilenceEnd()
    {
        //Debug.Log("OnSilenceEnd()");

        counter = 0f;
        textCounter.text = "00.0";
        silenceCounterState = SilenceCounterState.Idle;
        imageBackground.fillAmount = 0f;

        imageBackground.color = Color.grey;
        textCounter.color = Color.grey;
    }
}
