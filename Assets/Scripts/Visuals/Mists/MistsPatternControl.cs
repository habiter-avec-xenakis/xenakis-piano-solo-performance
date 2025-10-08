using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MistsPatternParameters
{
    public float angle;
    public float mirror;
    public float height;
}

public class MistsPatternControl : MonoBehaviour
{
    public MistsPatternParameters[] mistsPatternParameters;
    public Renderer patternRenderer;
    //public AnimationCurve curveMirror;

    //public float transitionTime = 3f;

    //[Range(0f,1f)]
    //public float mirrorTest;
    //private void Start()
    //{
    //    SetPattern(0);
    //}

    //private void Awake()
    //{
    //    patternRenderer = GetComponent<Renderer>();
    //}

    public void SetPattern(int index)
    {
        patternRenderer.material.SetFloat("Angle", mistsPatternParameters[index].angle);
        patternRenderer.material.SetFloat("Mirror", mistsPatternParameters[index].mirror);
        patternRenderer.transform.localPosition = new Vector3(patternRenderer.transform.localPosition.x, mistsPatternParameters[index].height, patternRenderer.transform.localPosition.z);
    }
}
