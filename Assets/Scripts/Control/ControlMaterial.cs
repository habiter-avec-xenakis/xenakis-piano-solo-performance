using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ControlMaterial : MonoBehaviour
{
    private Renderer r;
    public int materialIndex = 0;
    public string parameterName = "ParamName";

    private void Awake()
    {
        r = GetComponent<Renderer>();
    }

    public void SetValueFloat(string valueName, float value)
    {
        r.materials[materialIndex].SetFloat(valueName, value);
    }

    public void SetValueFloat(float value)
    {
        r.materials[materialIndex].SetFloat(parameterName, value);
    }
}
