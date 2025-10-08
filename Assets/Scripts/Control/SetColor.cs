using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SetColor : MonoBehaviour
{
    private Renderer rend;
    public string parameterName = "_BaseColor";

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        //Debug.Log("Rend " + rend);
    }

    public void SetAlpha(float value)
    {
        if(!rend)
        {
            return;
        }

        Color originalColor = rend.material.GetColor(parameterName);
        foreach (Material mat in rend.materials)
        {
            mat.SetColor(parameterName, new Color(originalColor.r, originalColor.g, originalColor.b, value));
        }
    }
}