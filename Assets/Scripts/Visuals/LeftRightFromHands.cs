using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightFromHands : MonoBehaviour
{
    [Header("Objects")]
    public Transform handLeft;
    public Transform handRight;
    public Transform boneReference;

    [Header("Parameters")]
    public float rawSmoothTime = 0.3f;
    public float normalizedSmoothTime = 0.3f;

    [Header("Cooldowns")]
    public bool useCooldown = false;
    public float averageMaxCooldown = 0.1f;

    // Raw Values

    private float _rawLeft = 0f;
    public float rawLeft
    {
        get { return _rawLeft; }
    }

    private float _rawRight = 0f;
    public float rawRight
    {
        get { return _rawRight; }
    }

    private float _maxLeft = 0f;
    public float maxLeft
    {
        get { return _maxLeft; }
    }

    private float _maxRight = 0f;
    public float maxRight
    {
        get { return _maxRight; }
    }

    private float _rawAverage;
    public float rawAverage
    {
        get { return _rawAverage; }
    }

    private float _rawAverageSmoothed;
    public float rawAverageSmoothed
    {
        get { return _rawAverageSmoothed; }
    }

    private float _smoothedVelocityRaw = 0f;

    private float _averageMaxLeft;
    public float averageMaxLeft
    {
        get { return _averageMaxLeft; }
    }

    private float _averageMaxRight;
    public float averageMaxRight
    {
        get { return _averageMaxRight; }
    }

    // Normalized Values

    private float _normalizedLeftRaw;
    public float normalizedLeft
    {
        get { return _normalizedLeftRaw; }
    }

    private float _normalizedRightRaw;
    public float normalizedRight
    {
        get { return _normalizedRightRaw; }
    }

    private float _normalizedAverageRaw;
    public float normalizedAverageRaw
    {
        get { return _normalizedAverageRaw; }
    }

    private float _normalizedLeftSmoothed;
    public float normalizedLeftSmoothed
    {
        get { return _normalizedLeftSmoothed; }
    }
    private float _normalizedLeftVelocity;

    private float _normalizedRightSmoothed;
    public float normalizedRightSmoothed
    {
        get { return _normalizedRightSmoothed; }
    }
    private float _normalizedRightVelocity;

    private float _normalizedAverageSmoothed;
    public float normalizedAverageSmoothed
    {
        get { return _normalizedAverageSmoothed; }
    }

    private float _smoothedVelocityNormalized;

    private void Update()
    {
        Vector3 pointLeft = boneReference.InverseTransformPoint(handLeft.position);
        Vector3 pointRight = boneReference.InverseTransformPoint(handRight.position);  

        _rawLeft = pointLeft.x;
        _rawRight = pointRight.x;

        _rawAverage = (_rawLeft + _rawRight) / 2f;

        if(useCooldown)
        {
            _averageMaxLeft += Time.deltaTime * averageMaxCooldown;
            _averageMaxRight -= Time.deltaTime * averageMaxCooldown;
        }

        if (_rawLeft < _maxLeft)
        {
            _maxLeft = _rawLeft;
        }

        if (_rawRight > _maxRight)
        {
            _maxRight = _rawRight;
        }

        if(_rawAverage < _averageMaxLeft)
        {
            _averageMaxLeft = _rawAverage;
        }

        if(_rawAverage > _averageMaxRight)
        {
            _averageMaxRight = _rawAverage;
        }

        float averageExtent = Mathf.Abs(_averageMaxLeft) + _averageMaxRight;
        float averageRatio = _rawAverage / averageExtent;
        _normalizedAverageRaw = averageRatio * 2f;
        _normalizedAverageRaw = Mathf.Clamp(_normalizedAverageRaw, -1f, 1f);

        _rawAverageSmoothed = Mathf.SmoothDamp(_rawAverageSmoothed, _rawAverage, ref _smoothedVelocityRaw, rawSmoothTime);

        _normalizedAverageSmoothed = Mathf.SmoothDamp(_normalizedAverageSmoothed, _normalizedAverageRaw, ref _smoothedVelocityNormalized, normalizedSmoothTime);

        float maxExtent = -maxRight + maxLeft;

        _normalizedLeftRaw = _rawLeft / maxExtent;
        _normalizedRightRaw = -_rawRight / maxExtent;

        _normalizedLeftSmoothed = Mathf.SmoothDamp(_normalizedLeftSmoothed, _normalizedLeftRaw, ref _normalizedLeftVelocity, normalizedSmoothTime);
        _normalizedRightSmoothed = Mathf.SmoothDamp(_normalizedRightSmoothed, _normalizedRightRaw, ref _normalizedRightVelocity, normalizedSmoothTime);
    }
}