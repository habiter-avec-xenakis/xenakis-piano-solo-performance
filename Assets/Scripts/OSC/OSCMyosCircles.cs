using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCMyosCircles : MonoBehaviour
{
    [Header("OSC Myos Script")]
    public OSCMyos oscMyos;

    [Header("Lines")]
    public bool createLines = false;
    public Material linesMaterial;
    public float widthMultiplier = 0.15f;
    public float linesOffset;
    public bool useLinesColors = false;
    public Color ColorLineLeft = Color.red;
    public Color ColorLineRight = Color.blue;

    [Header("Circles")]
    public float radius = 2f;
    public float radiusOffset = 1f;
    public float offset = 1f;
    [Range(0.25F, 2f)]
    public float ratio= 1f;
    public float valueMultiplier = 1f;

    [Header("Circle objects")]
    public float visObjectsScale = 0.05f;
    public Material visObjectsMaterial;
    public bool usePrefab = false;
    public GameObject prefab;
    public Transform lookAtTarget;

    [Header("Control")]
    private Renderer[] visObjectRenderers;
    [Range(0f, 1f)]
    public float globalOpacity;

    private Vector3[] lPositions = new Vector3[8];
    private Vector3[] rPositions = new Vector3[8];

    private Transform[] lTransforms = new Transform[8];
    private Transform[] rTransforms = new Transform[8];

    private LineRenderer lLine;
    private LineRenderer rLine;

    private void Start()
    {
        lTransforms = GetTransforms("Myo_Left");
        rTransforms = GetTransforms("Myo_Right");
        visObjectRenderers = GetComponentsInChildren<Renderer>();

        if(createLines)
        {
            lLine = GetLine("Line_Left");
            rLine = GetLine("Line_Right");

            lLine.gameObject.layer = gameObject.layer;
            rLine.gameObject.layer = gameObject.layer;

            lLine.useWorldSpace = false;
            rLine.useWorldSpace = false;

            if(useLinesColors)
            {
                lLine.material.SetColor("_UnlitColor", ColorLineLeft);
                rLine.material.SetColor("_UnlitColor", ColorLineRight);
            }
        }
    }

    private Transform[] GetTransforms(string prefix)
    {
        Transform[] transforms = new Transform[8];

        for(int i = 0; i < 8; i++)
        {
            var vo = GetVisualisationObject(prefix + "_" + i);
            vo.layer = gameObject.layer;
            transforms[i] = vo.transform;
        }

        return transforms;
    }

    private LineRenderer GetLine(string name)
    {
        var trailObject = new GameObject();
        trailObject.name = name;
        trailObject.transform.SetParent(transform, false);

        LineRenderer line = trailObject.AddComponent<LineRenderer>();
        line.material = linesMaterial;
        line.widthMultiplier = 0.15f;
        line.positionCount = 8;
        line.loop = true;
        line.alignment = LineAlignment.TransformZ;
        line.textureMode = LineTextureMode.Tile;

        return line;
    }

    private void Update()
    {
        SetTransformsPositions(lTransforms, lPositions, oscMyos.lForce8, -offset);
        SetTransformsPositions(rTransforms, rPositions, oscMyos.rForce8, offset);

        if(createLines)
        {
            lLine.SetPositions(lPositions);
            rLine.SetPositions(rPositions);
        }

        for(int i = 0; i < 8; i++)
        {
            lTransforms[i].localScale = Vector3.one * visObjectsScale;
            rTransforms[i].localScale = Vector3.one * visObjectsScale;
        }

        if(createLines)
        {
            lLine.widthMultiplier = widthMultiplier;
            rLine.widthMultiplier = widthMultiplier;
        }

        lLine.material.SetColor("_UnlitColor", new Color(ColorLineLeft.r, ColorLineLeft.g, ColorLineLeft.b, globalOpacity));
        rLine.material.SetColor("_UnlitColor", new Color(ColorLineRight.r, ColorLineRight.g, ColorLineRight.b, globalOpacity));
        foreach (var r in visObjectRenderers)
        {
            r.material.SetColor("_UnlitColor", new Color(0f, 0f, 0f, globalOpacity));
        }
    }

    private void SetTransformsPositions(Transform[] transforms, Vector3[] positions, float[] values, float offset)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI * 2f / 8f;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius * (values[i] * valueMultiplier + radiusOffset) + offset, Mathf.Sin(angle) * (radius * ratio) * (values[i] * valueMultiplier + radiusOffset), 0f);
            positions[i] = pos;
            transforms[i].localPosition = pos;
        }
    }

    private GameObject GetVisualisationObject(string name)
    {
        GameObject visObject;

        if (usePrefab)
        {
            visObject = Instantiate(prefab);
            var lookAt = visObject.GetComponent<LookAt>();
            if (lookAt != null)
            {
                lookAt.target = lookAtTarget;
            }
        }
        else
        {
            visObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visObject.GetComponent<Renderer>().material = visObjectsMaterial;
        }

        visObject.name = name;
        visObject.transform.SetParent(transform, false);
        visObject.transform.localScale = Vector3.one * visObjectsScale;

        return visObject;
    }

    public void SetCirclesOpacity(float value)
    {
        globalOpacity = value;
    }
}