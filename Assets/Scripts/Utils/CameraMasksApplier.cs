using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetMask { Front, Ground, Left, Right }

[RequireComponent(typeof(Image))]
public class CameraMasksApplier : MonoBehaviour
{
    private Image image;
    public TargetMask targetMask;

    private void Awake()
    {
        image = GetComponent<Image>();
        var sprite = PerformanceGlobalManager.cameraMasks[(int)targetMask];
        if(sprite)
        {
            image.sprite = sprite;
        }
        else
        {
            image.enabled = false;
        }
    }
}