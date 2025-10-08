using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class ControlVFX : MonoBehaviour
{
    private VisualEffect visualEffect;

    [Range(0f, 1f)]
    public float chaos;

    public ControlVFXGroup controlVFXGroup;
    private void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        visualEffect.SetFloat("mtChaos", chaos * 3f);
    }

    public void SetChaos(float value)
    {
        chaos = value;
    }
}
