using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float angleX = Mathf.PingPong(Time.time, 360) * speed;
        float angleY = Mathf.PingPong(Time.time + 120, 360) * speed;
        float angleZ = Mathf.PingPong(Time.time + 240, 360) * speed;

        transform.localEulerAngles = new Vector3(angleX, angleY, angleZ);
    }
}
