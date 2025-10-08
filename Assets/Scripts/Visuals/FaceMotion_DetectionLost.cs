using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Smrvfx;

public class FaceMotion_DetectionLost : MonoBehaviour
{
    public SkinnedMeshBaker smb;
    public Transform parent;
    //public Vector3 offset;
    public int targetVertex;
    private GameObject debugObject;
    public bool debugOn;

    private void Start()
    {
        debugObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugObject.transform.localScale = Vector3.one * 0.025f;
        debugObject.transform.SetParent(parent);
    }

    private void Update()
    {
        if(debugOn)
        {
            debugObject.transform.localPosition = smb.vertices[targetVertex];
        }
    }
}
