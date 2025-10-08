using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Smrvfx;

public class BakerVertexCountToVFX : MonoBehaviour
{
    public SkinnedMeshBaker smb;
    public VisualEffect vfx;
    private void Start()
    {
        vfx.SetInt("VertexCount", smb.VertexCount);
    }
}
