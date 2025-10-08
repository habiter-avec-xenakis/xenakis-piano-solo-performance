using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Smrvfx;

public class AttachToSkinnedMeshVertex : MonoBehaviour
{
    public SkinnedMeshBaker smb;
    public int vertexIndex;
    public Vector3 offset;
    private bool active = false;
    public bool useRotation = true;

    public bool noise = false;
    public float noiseAmount = 0.01f;
    public float noiseSpeed = 10f;
    private float noiseMultitplier;

    private Vector3 noiseSeed;

    private void Start()
    {
        noiseSeed = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    private void Update()
    {
        if(!active)
        {
            active = true;
            return;
        }

        int vIndex = Mathf.Clamp(vertexIndex, 0, smb.VertexCount);

        Vector3 noiseVector = Vector3.zero;

        if(noise)
        {
            float t = Time.time * noiseSpeed;
            noiseVector = new Vector3(Mathf.PerlinNoise(t, noiseSeed.x), Mathf.PerlinNoise(t, noiseSeed.y), Mathf.PerlinNoise(t, noiseSeed.z)) * noiseAmount;

        }

        transform.localPosition = smb.vertices[vIndex] + offset + noiseVector;
        if(useRotation)
        {
            transform.localRotation = Quaternion.LookRotation(smb.normals[vIndex], transform.up);
            Debug.DrawRay(smb.vertices[vIndex] + offset, smb.normals[vIndex]);
        }
    }
}