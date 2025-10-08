using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITimeCounter : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float startTime;
    private bool counting = false;
    public bool autoStart = false;
    public string label = "Total time: ";

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.enabled = false;
        if(autoStart)
        {
            BeginCounting();
        }
    }

    public void BeginCounting()
    {
        startTime = Time.realtimeSinceStartup;
        counting = true;
        timerText.enabled = true;
    }

    private void Update()
    {
        if(counting)
        {
            timerText.text = label + FormattedTime(Time.realtimeSinceStartup - startTime);
        }
    }

    private string FormattedTime(float value)
    {
        return string.Format("{0:#0}:{1:00}.{2:00}",
                     Mathf.Floor(value / 60),//minutes
                     Mathf.Floor(value) % 60,//seconds
                     Mathf.Floor((value * 100) % 100));//miliseconds
    }
}
