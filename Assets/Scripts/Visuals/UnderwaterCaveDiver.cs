using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterCaveDiver : MonoBehaviour
{
    private Vector3 position;
    private Vector3 positionOrigin;
    private Vector3 rotation;
    public Vector3 positionOffset;

    [Header("Random Translation")]
    public bool useRandomTranslation;
    public float randomTranslationSpeed = 1f;
    public float randomTranslationDistance = 1f;
    private float randomTranslationTime = 0f;

    [Header("Random Rotation")]
    public bool useRandomRotation;
    public float randomRotationSpeed = 1f;
    public float randomRotationAngle = 15f;
    private float randomRotationTime;

    [Header("Roll")]
    public float roll = 0f;

    [Header("Smoothing")]
    public bool smoothPosition;
    private Vector3 positionSmooth;
    public float smoothPositionTime = 0.3f;
    private Vector3 positionVelocity = Vector3.zero;

    public bool smoothRotation;
    private Vector3 rotationSmooth;
    public float smoothRotationTime = 0.3f;
    private Vector3 rotationVelocity = Vector3.zero;

    [Header("Breath")]
    public bool useBreath;
    private float _breathNormalized;
    public float breathNormalized
    {
        get { return _breathNormalized; }
    }
    public float breathMultiplier = 1f;
    public float breathSpeed = 1f;
    public float breathPhase = 1f;
    public float breathPow = 1f;
    private float breathTime = 0f;

    private void Start()
    {
        positionOrigin = transform.localPosition;
    }

    private void Update()
    {
        position = positionOrigin;
        rotation = Vector3.zero;

        // Breath
        if (useBreath)
        {
            float breath = Mathf.Clamp(Mathf.Sin(breathTime) * breathPhase, -1f, 1f);
            _breathNormalized = breath / 2f + 0.5f;
            breathTime += Time.deltaTime * breathSpeed;
            position += new Vector3(0, breath * breathMultiplier, 0);
        }

        // Apply random translation
        if(useRandomTranslation)
        {
            Vector3 randomTranslation = new Vector3 (Mathf.PerlinNoise(randomTranslationTime, 0F),  Mathf.PerlinNoise(randomTranslationTime, 2.3f), 0f) * randomTranslationDistance - new Vector3(randomTranslationDistance / 2f, randomTranslationDistance / 2f, 0f);
            randomTranslationTime += Time.deltaTime * randomTranslationSpeed;
            position += randomTranslation;
        }

        // Apply random rotation
        if(useRandomRotation)
        {
            Vector3 randomRotation = new Vector3(Mathf.PerlinNoise(randomRotationTime, 4.77f), Mathf.PerlinNoise(randomRotationTime, 1.87f), Mathf.PerlinNoise(randomRotationTime, 6.42f)) * randomRotationAngle * 2 - (Vector3.one * randomRotationAngle);
            randomRotationTime += Time.deltaTime * randomRotationSpeed;
            rotation += randomRotation;
        }

        position += positionOffset;

        // Apply roll
        rotation += new Vector3(0, 0, roll);

        // Apply smooth
        positionSmooth = Vector3.SmoothDamp(positionSmooth, position, ref positionVelocity, smoothPositionTime);
        rotationSmooth = Vector3.SmoothDamp(rotationSmooth, rotation, ref rotationVelocity, smoothRotationTime);

        if (smoothPosition)
        {
            transform.localPosition = positionSmooth;
            transform.localEulerAngles = rotationSmooth;
        }
        else
        {
            transform.localPosition = position;
            transform.localEulerAngles = rotation;
        }
    }
    public void SetRoll(float value)
    {
        roll = value;
    }
}
