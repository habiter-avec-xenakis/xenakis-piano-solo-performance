using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DistanceToVisuals : MonoBehaviour
{
    [Header("Objects")]
    public Transform transformA;
    public Transform transformB;
    public VisualEffect absDistanceEffect;
    public bool setPosition = false;
    public string paramPosName;
    public bool setDistance = false;
    public string paramDistName;
    private Vector3 _handsCenter;
    public Vector3 handsCenter
    {
        get { return _handsCenter; }
    }

    [Header("Values")]
    public float valueRaw;

    private float _valueMin = 100f;
    public float valueMin
    {
        get { return _valueMin; }
    }

    private float _valueMaxSmoothed = 0f;
    public float valueMaxSmoothed
    {
        get { return _valueMaxSmoothed; }
    }

    private float _valueMax = 0f;
    public float valueMax
    {
        get { return _valueMax; }
    }

    public float valueNormalized;
    public bool valuesMinMaxSmoothing = false;
    public float valuesMinMaxSmoothingSpeed = 0.1f;

    public int frameCount = 1;
    public double valueTotal = 0;


    private void Update()
    {
        float currentDistance = Vector3.Distance(transformA.position, transformB.position);

        valueRaw = currentDistance;

        if (currentDistance < _valueMin)
        {
            _valueMin = currentDistance;
        }

        if (currentDistance > _valueMaxSmoothed)
        {
            _valueMaxSmoothed = currentDistance;
        }

        if (currentDistance > _valueMax)
        {
            _valueMax = currentDistance;
        }

        valueNormalized = (currentDistance - _valueMin) / (_valueMaxSmoothed - _valueMin);
        if (valuesMinMaxSmoothing)
        {
            _valueMaxSmoothed -= valuesMinMaxSmoothingSpeed * Time.deltaTime;
            _valueMin += valuesMinMaxSmoothingSpeed * Time.deltaTime;
        }

        frameCount++;

        _handsCenter = Vector3.Lerp(transformA.position, transformB.position, 0.5f);
        //Debug.Log(_handsCenter);

        if (absDistanceEffect)
        {
            if(setPosition)
            {
                absDistanceEffect.SetVector3(paramPosName, _handsCenter);
            }
            if(setDistance)
            {
                absDistanceEffect.SetFloat(paramDistName, valueNormalized);
            }
        }
    }
}