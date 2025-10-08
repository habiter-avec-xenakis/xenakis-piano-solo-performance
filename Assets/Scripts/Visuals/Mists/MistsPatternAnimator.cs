using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistsPatternAnimator : MonoBehaviour
{
    public float speed;
    public float killDistance;

    private void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if(transform.position.z <= killDistance)
        {
            Destroy(this.gameObject);
        }
    }
}