using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScale : MonoBehaviour
{
     [Range(0f,1f)]
    public float scale = 0f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    private Vector3 m_scaleOriginal;
    private Vector3 m_scaleCurrent;

    public Vector3 scaleCurrent
    {
        get { return m_scaleCurrent; }
    }

    private void Awake()
    {
        m_scaleOriginal = transform.localScale;
    }

    private void Update()
    {
        m_scaleCurrent = m_scaleOriginal * (minScale + ((maxScale - minScale)) * scale);
        transform.localScale = m_scaleCurrent;
    }

    public void SetScale(float value)
    {
        scale = value;
    }
}
