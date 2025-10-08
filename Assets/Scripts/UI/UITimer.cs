using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool autoStart;
    private float time;
    private bool counting = false;

    private void Start()
    {
        if(autoStart)
        {
            TimerStart();
        }
    }

    private void Update()
    {
        if(counting)
        {
            time += Time.deltaTime;
            SetText();
        }
    }

    private void SetText()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimerStart()
    {
        counting = true;
    }

    public void TimerStop()
    {
        counting = false;
    }

    public void TimerReset()
    {
        time = 0f;
        SetText();
    }
}
