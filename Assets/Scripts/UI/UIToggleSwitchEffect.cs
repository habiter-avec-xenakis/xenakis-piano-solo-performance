using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleSwitchEffect : MonoBehaviour
{
    private Toggle toggle;
    public RectTransform targetTransform;
    public float switchedValue = 10f;
    private float baseValue;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
        baseValue = targetTransform.offsetMin.x;
    }

    private void OnValueChanged(bool value)
    {
        targetTransform.offsetMin = new Vector2(1, 2);
        targetTransform.offsetMax = new Vector2(3, 4);

        if (value)
        {
            targetTransform.offsetMin = new Vector2(switchedValue, baseValue);
            targetTransform.offsetMax = new Vector2(baseValue, baseValue);
        }
        else
        {
            targetTransform.offsetMin = new Vector2(baseValue, baseValue);
            targetTransform.offsetMax = new Vector2(switchedValue, baseValue);
        }
    }
}