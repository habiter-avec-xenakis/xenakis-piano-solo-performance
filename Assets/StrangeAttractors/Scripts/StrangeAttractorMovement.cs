using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum StrangeAttractorType { Lorenz, Dadras, Aizawa, Thomas, Chen, Lorenz83, Rossler, Halvorsen, RabinovichFabrikant, ThreeScrollUnifiedChaoticSystem, Sprott, FourWings }

public class StrangeAttractorMovement : MonoBehaviour
{
    public StrangeAttractorType strangeAttractorType;
    public float speed = 1f;
    public float scale = 1f;
    public Vector3 offset;
    public Vector3 rotation;

    private Vector3 pos;
    public StrangeAttractor strangeAttractor;

    private void Start()
    {
        SetStrangeAttractor(strangeAttractorType);
        offset = transform.localPosition;
        pos = Random.insideUnitSphere * 0.25f;
    }

    private void Update()
    {
        Matrix4x4 m = Matrix4x4.Scale(Vector3.one * scale);
        Matrix4x4 mtrs = Matrix4x4.TRS(offset, Quaternion.Euler(rotation), Vector3.one * scale);
        pos = strangeAttractor.GetNewPosition(pos, Time.deltaTime * speed);
        Vector3 posScaled = mtrs.MultiplyPoint3x4(pos);
        transform.localPosition = posScaled;
        transform.localRotation = strangeAttractor.rotation;
    }

    public void SetStrangeAttractor(StrangeAttractorType type)
    {
        strangeAttractor = StrangeAttractorUtils.GetStrangeAttractorFromType(type);
    }
}