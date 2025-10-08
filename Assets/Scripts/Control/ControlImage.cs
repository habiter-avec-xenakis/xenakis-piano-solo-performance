using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ColorTools;

[RequireComponent(typeof(Image))]
public class ControlImage : MonoBehaviour
{
    private Image image;

    [Range(0f, 1f)]
    public float alpha;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.color = ColorTools.ColorModifiers.ColorAlpha(image.color, 1f - alpha);
    }

    public void SetAlpha(float value)
    {
        alpha = value;
    }
}
