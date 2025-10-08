using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class UISliderSetTextValues : MonoBehaviour
{
    public TextMeshProUGUI valueMin;
    public TextMeshProUGUI valueMax;

    private void Awake()
    {
        var slider = GetComponent<Slider>();

        if(valueMin)
        {
            valueMin.text = slider.minValue.ToString("00.00");
            valueMax.text = slider.maxValue.ToString("00.00");
        }

        //slider.onValueChanged.Invoke(slider.value);
    }
}