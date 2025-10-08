using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFacemotionChecker : MonoBehaviour
{
    public BlendShapesWeightsManager shapesManager;
    public Image imageFill;
    public float valueMultiplier = 1f;
    private Color colorOriginal;
    public Color colorDetectionLost = Color.grey;
    public Image imageCheck;
    public bool detected;

    public void Start()
    {
        colorOriginal = imageFill.color;

        OnDetectionLost();
    }

    private void Update()
    {
        if(detected)
        {
            imageFill.fillAmount = shapesManager.average * valueMultiplier;
        }
    }

    public void OnDetected()
    {
        detected = true;
        imageFill.color = colorOriginal;
        imageCheck.enabled = true;
        imageFill.fillAmount = shapesManager.average * valueMultiplier;
    }

    public void OnDetectionLost()
    {
        detected = false;
        imageFill.color = colorDetectionLost;
        imageFill.fillAmount = 1f;
        imageCheck.enabled = false;
    }
}