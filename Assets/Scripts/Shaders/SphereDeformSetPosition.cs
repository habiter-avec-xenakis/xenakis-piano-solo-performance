using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDeformSetPosition : MonoBehaviour
{
    public Renderer[] renderers;
    public Transform deformTransform;

    private void Update()
    {
        foreach(var r in renderers)
        {
            r.material.SetVector("_DeformPos", deformTransform.position);
        }
    }
}
