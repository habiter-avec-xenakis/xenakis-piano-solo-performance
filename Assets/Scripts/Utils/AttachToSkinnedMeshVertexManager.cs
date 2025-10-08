using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Smrvfx;

public class AttachToSkinnedMeshVertexManager : MonoBehaviour
{
    //public FaceMotion_DetectionLost detectinoLost;
    public GameObject prefab;
    public Transform lookAtTarget;
    public SkinnedMeshBaker smb;
    public int[] vIndices;
    public Vector3 offset;
    public TextAsset textAsset;
    public bool useRotation;
    private List<GameObject> instancedObjects = new List<GameObject>();
    private Renderer[] renderers;
    private AttachToSkinnedMeshVertex[] attaches;
    private Transform parent;

    [Range(0f,1f)]
    public float vertexAlpha = 1f;

    [Range(0f, 1f)]
    public float vertexWhiteLerp = 0f;

    [Range(0f, 10f)]
    public float vertexMultiplier = 1f;

    public bool setColor = false;
    public Color vertexColor;

    private void Start()
    {
        GameObject parentObject = new GameObject();
        parentObject.name = "VerticesAttachments";
        parent = parentObject.transform;

        List<Renderer> rends = new List<Renderer>();
        List<AttachToSkinnedMeshVertex> att = new List<AttachToSkinnedMeshVertex>();

        if(textAsset)
        {
            string[] vIndicesString = textAsset.text.Split(';');
            vIndices = new int[vIndicesString.Length - 1];
            for (int i = 0; i < vIndicesString.Length - 1; i++)
            {
                //Debug.Log(vIndicesString[i]);
                vIndices[i] = int.Parse(vIndicesString[i]);
            }
        }

        for(int i = 0; i < vIndices.Length; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.name = "VertexAttachment_" + vIndices[i].ToString("000");
            AttachToSkinnedMeshVertex attachComponent = instance.AddComponent<AttachToSkinnedMeshVertex>();
            attachComponent.smb = smb;
            attachComponent.offset = offset;
            attachComponent.vertexIndex = vIndices[i];
            attachComponent.useRotation = useRotation;
            att.Add(attachComponent);

            var lookAt = instance.GetComponent<LookAt>();
            if(lookAt)
            {
                lookAt.target = lookAtTarget;
            }

            var rend = instance.GetComponent<Renderer>();
            rends.Add(rend);
            if (setColor)
            {
                rend.material.SetColor("_EmissiveColor", vertexColor);
                rend.material.SetColor("_BaseColor", Color.black);
            }

            instance.transform.SetParent(parent);
            instancedObjects.Add(instance);
            //if(detectinoLost.tranformRef == null)
            //{
            //    detectinoLost.tranformRef = instance.transform;
            //}
        }

        renderers = rends.ToArray();

        SetAlpha(vertexAlpha);
    }

    public void SetAlpha(float value)
    {
        vertexAlpha = value;
        SetColor();
    }

    public void SetVertexWhiteLerp(float value)
    {
        vertexWhiteLerp = value;
        SetColor();
    }

    public void SetMultiplier(float value)
    {
        vertexMultiplier = 1f + 9f * value;
        SetColor();
    }

    //public void OnDetectionLost()
    //{
    //    Debug.Log("Lost");
    //}

    //public void OnDetected()
    //{
    //    Debug.Log("Found");
    //}

    private void SetColor()
    {
        Color col = Color.Lerp(vertexColor, Color.white, vertexWhiteLerp);
        foreach (var r in renderers)
        {
            r.material.SetColor("_BaseColor", new Color(0f, 0f, 0f, vertexAlpha));
            r.material.SetColor("_EmissiveColor", col * vertexMultiplier);
        }
    }

    public void DestroyInstances()
    {
        foreach(var instance in instancedObjects)
        {
            Destroy(instance);
        }
    }
}
