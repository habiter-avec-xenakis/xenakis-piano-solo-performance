using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoudnessTransform : AudioSourceLoudness
{
    [Header("Transform")]
    public Transform target;

    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public Vector3 targetScale = Vector3.one;

    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Vector3 originalScale;

    private void Start()
    {
        originalPosition = target.localPosition;
        originalRotation = target.localEulerAngles;
        originalScale = target.localScale;
    }

    public override void Update()
    {
        base.Update();
        float loudness = Mathf.Clamp(clipLoudness, 0f, loudnessCap);
        loudness = loudness * (1f / loudnessCap);

        target.localPosition = Vector3.Lerp(originalPosition, targetPosition, loudness);
    }
}
