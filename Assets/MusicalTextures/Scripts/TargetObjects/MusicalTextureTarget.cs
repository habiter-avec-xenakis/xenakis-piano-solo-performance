using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetMode { Main, Secondary, Blend }
public class MusicalTextureTarget : MonoBehaviour
{
    protected MusicalTextureManager mtManager;
    public TargetMode targetMode;

    protected virtual void Start()
    {
        mtManager = FindObjectOfType<MusicalTextureManager>();
    }
}
