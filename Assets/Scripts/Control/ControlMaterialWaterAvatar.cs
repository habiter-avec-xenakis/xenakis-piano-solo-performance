using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ControlMaterial))]
public class ControlMaterialWaterAvatar : MonoBehaviour
{
    private ControlMaterial cMat;

    public Slider sliderDisplacement;
    public Slider sliderDisplacementScale;
    public Slider sliderFresnelPower;
    public Slider sliderSpeedMain;
    public Slider sliderSpeedSecondary;

    private void Start()
    {
        cMat = GetComponent<ControlMaterial>();

        if(sliderDisplacement)
        {
            SetSider(sliderDisplacement, "_Displacement");
        }

        if (sliderDisplacementScale)
        {
            SetSider(sliderDisplacementScale, "_DisplacementScale");
        }

        if (sliderFresnelPower)
        {
            SetSider(sliderFresnelPower, "_FresnelPower");
        }

        if (sliderSpeedMain)
        {
            SetSider(sliderSpeedMain, "_SpeedMain");
        }

        if (sliderSpeedSecondary)
        {
            SetSider(sliderSpeedSecondary, "_SpeedSecondary");
        }
    }

    private void SetSider(Slider slider, string valueName)
    {
        slider.onValueChanged.AddListener(delegate { cMat.SetValueFloat(valueName, slider.value); });
        slider.onValueChanged.Invoke(slider.value);
    }
}
