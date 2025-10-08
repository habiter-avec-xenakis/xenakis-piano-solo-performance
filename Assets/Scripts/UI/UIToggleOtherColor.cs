using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleOtherColor : MonoBehaviour
{
    private Toggle toggle;
    public Graphic graphic;
    private Color colorOriginal;
    public Color colorSelected = Color.red;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        colorOriginal = graphic.color;
        toggle.onValueChanged.AddListener(OnToggleSwitch);
    }

    private void OnToggleSwitch(bool value)
    {
        if(value)
        {
            graphic.color = colorSelected;
        }

        else
        {
            graphic.color = colorOriginal;
        }
    }
}