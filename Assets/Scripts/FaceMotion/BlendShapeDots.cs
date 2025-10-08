using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeDots : MonoBehaviour
{
    [Header("Main")]
    public BlendShapesWeightsManager blendShapesWeightManager;
    public GameObject prefab;

    [Header("Array")]
    public int columns = 10;
    public float space = 0.1f;

    [Header("Material")]
    [ColorUsageAttribute(true,true)]
    public Color colorA = Color.blue;
    [ColorUsageAttribute(true, true)]
    public Color colorB = Color.red;

    //public float emissiveMultiplier = 5f;

    private Transform root;
    private Renderer[] renderers;

    private void Start()
    {
        GameObject rootObject = new GameObject();
        rootObject.name = "Dots";
        root = rootObject.transform;

        int rows = blendShapesWeightManager.weights.Length / columns;
        int remainder = blendShapesWeightManager.weights.Length % columns;
        renderers = new Renderer[blendShapesWeightManager.weights.Length];

        if(remainder > 0)
        {
            rows++;
        }

        Vector3 offset = new Vector3(space * (columns - 1), -space * (rows - 1), 0) / 2f;

        int count = 0;
        for (int r = 0; r < rows; r++)
        {
            for(int c = 0; c < columns; c++)
            {
                if(count >= blendShapesWeightManager.weights.Length)
                {
                    continue;
                }

                var prefabInstance = Instantiate(prefab);
                prefabInstance.transform.SetParent(root, false);
                prefabInstance.transform.localPosition = new Vector3(c * space, -r * space, 0) - offset;
                prefabInstance.name = "Dot_" + count.ToString("00");

                renderers[count] = prefabInstance.GetComponentInChildren<Renderer>();

                count++;
            }
        }
    }

    private void Update()
    {
        for(int i = 0; i < renderers.Length; i++)
        {
            float weight = blendShapesWeightManager.weights[i];

            renderers[i].material.SetColor("_UnlitColor", new Color(1.5f, 1.5f, 1.5f, weight));
        }
    }
}