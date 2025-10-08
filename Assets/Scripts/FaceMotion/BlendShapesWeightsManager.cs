using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlendShapesWeightsManager : MonoBehaviour
{
    public TextAsset blendShapeList;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform head;
    private Mesh mesh;
    private float[] _weights;
    public float[] weights
    {
        get { return _weights; }
    }
    private int[] _indices;

    private float _average;
    public float average
    {
        get { return _average; }
    }
    private float _lastAverage;

    private float _acceleration;
    public float acceleration
    {
        get { return _acceleration; }
    }
    private float _lastAcceleration;
    private float _maxAcceleration;
    private float _normalizedAcceleration;
    public float normalizedAcceleration
    {
        get { return _normalizedAcceleration; }
    }

    public bool controlOtherManagers = false;
    public BlendShapesWeightsManager[] blendShapesManagersControl;

    public UnityEvent onDetected = new UnityEvent();
    public UnityEvent onDetectionLost = new UnityEvent();

    private void Awake()
    {
        mesh = skinnedMeshRenderer.sharedMesh;
        var lines = blendShapeList.text.Split("\n"[0]);

        _indices = new int[mesh.blendShapeCount];
        _weights = new float[mesh.blendShapeCount];

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            if (line == "")
            {
                continue;
            }

            for (int j = 0; j < mesh.blendShapeCount; j++)
            {
                var blendShapeName = mesh.GetBlendShapeName(j);

                if (blendShapeName == line)
                {
                    _indices[i] = j;
                }
            }
        }
    }

    private void Update()
    {
        for(int i = 0; i < mesh.blendShapeCount; i++)
        {
            _weights[i] = skinnedMeshRenderer.GetBlendShapeWeight(_indices[i]) / 100f;
        }
        _average = GetAverage(weights);

        if(controlOtherManagers)
        {
            foreach(var control in blendShapesManagersControl)
            {
                control.SetWeights(_weights);
                control.SetRotation(head.localRotation);
            }
        }

        _acceleration = (_average - _lastAverage) * Time.deltaTime;

        if(_acceleration != _lastAcceleration)
        {
            if(_lastAcceleration == 0f)
            {
                onDetected.Invoke();
            }
            else if(_acceleration == 0f)
            {
                onDetectionLost.Invoke();
            }
        }

        if(_acceleration > _maxAcceleration)
        {
            _maxAcceleration = _acceleration;
        }

        _normalizedAcceleration = Mathf.Abs(_acceleration) / _maxAcceleration;

        _lastAcceleration = _acceleration;
        _lastAverage = _average;
    }

    private float GetAverage(float[] values)
    {
        float sum = 0f;
        for(int i = 0; i < values.Length; i++)
        {
            sum += values[i];
        }
        return sum / values.Length;
    }

    public void SetWeights(float[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_indices[i], values[i] * 100f);
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        head.localRotation = rotation;
    }

    public void SetOthersControl(bool value)
    {
        controlOtherManagers = value;
    }
}