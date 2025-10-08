using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeAttractor
{
    protected Vector3 lastPosition = Vector3.zero;
    protected Quaternion _rotation = Quaternion.identity;

    protected float x;
    protected float y;
    protected float z;

    private float limit = 10000f;

    public Quaternion rotation
    {
        get { return _rotation; }
    }

    public virtual Vector3 GetNewPosition(Vector3 position, float t)
    {
        CalculatePosition(position, t);
        CalculateRotation(position);
        lastPosition = position;
        return new Vector3(Mathf.Clamp(x, -limit, limit), Mathf.Clamp(y, -limit, limit), Mathf.Clamp(z, -limit, limit));
    }

    protected virtual void CalculatePosition(Vector3 position, float t)
    {

    }

    protected void CalculateRotation(Vector3 position)
    {
        Vector3 relativePosition = position - lastPosition;
        if(Vector3.Magnitude(relativePosition) > 0f)
        {
            _rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        }
    }
}