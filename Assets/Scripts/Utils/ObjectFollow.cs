using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform target;

    [Header("Position")]
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    public Vector3 offset = Vector3.zero;

    [Header("Rotation")]
    public bool useRotation = false;

    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    [Header("Options")]
    public bool smooth = false;
    public float smoothTime = 0.3f;

    private Vector3 positionSmooth;
    private Vector3 smoothVelocity = Vector3.zero;

    private void Update()
    {
        if(!target)
        {
            return;
        }

        Vector3 positionNew = Vector3.zero;

        if (rotateX)
        {
            transform.localEulerAngles = new Vector3(target.eulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        if (rotateY)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.eulerAngles.y, transform.localEulerAngles.z);
        }

        if (rotateZ)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, target.eulerAngles.z);
        }

        if (followX)
        {

            positionNew = new Vector3(target.position.x, positionNew.y, positionNew.z);
        }

        if (followY)
        {
            positionNew = new Vector3(positionNew.x, target.position.y, positionNew.z);
        }

        if (followZ)
        {
            positionNew = new Vector3(positionNew.x, positionNew.y, target.position.z);
        }

        positionNew += transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z;


        positionSmooth = Vector3.SmoothDamp(positionSmooth, positionNew, ref smoothVelocity, smoothTime);

        if(smooth)
        {
            transform.position = positionSmooth;
        }
        else
        {
            transform.position = positionNew;
        }
    }
}