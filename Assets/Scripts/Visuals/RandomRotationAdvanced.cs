using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationAdvanced : MonoBehaviour
{
    public bool useRandomRotation;
    public float speed = 1f;
    public float smoothSpeed = 1f;

    private Quaternion rotationOriginal;
    private Quaternion rotationRandom;

    private void Awake()
    {
        rotationOriginal = transform.rotation;
    }

    private void Update()
    {
        float angleX = Mathf.PingPong(Time.time, 360) * speed;
        float angleY = Mathf.PingPong(Time.time + 120, 360) * speed;
        float angleZ = Mathf.PingPong(Time.time + 240, 360) * speed;

        rotationRandom = Quaternion.Euler(new Vector3(angleX, angleY, angleZ));

        var step = smoothSpeed * Time.deltaTime;

        if (useRandomRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationRandom, step);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationOriginal, step);
        }
    }
}