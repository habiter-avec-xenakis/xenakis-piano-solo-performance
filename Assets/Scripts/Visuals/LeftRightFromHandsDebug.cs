using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LeftRightFromHands))]
public class LeftRightFromHandsDebug : MonoBehaviour
{
    private LeftRightFromHands lrfh;

    // GameObjects
    private GameObject debugLeft;
    private GameObject debugRight;
    private GameObject debugBone;
    private GameObject debugLeftMax;
    private GameObject debugRightMax;

    private GameObject debugAverageRaw;
    private GameObject debugAverageMaxLeft;
    private GameObject debugAverageMaxRight;
    private GameObject debugAverageSmoothed;

    [Header("Debug")]
    public float debugOffset = 0.25f;
    public Material debugMatCenter;
    public Material debugMatLeft;
    public Material debugMatRight;
    public Material debugMatMax;
    public Material debugMatAverageRaw;
    public Material debugMatAverageMax;
    public Material debugMatAverageSmoothed;

    // Activation
    public bool debugOn = true;
    private List<Renderer> renderers = new List<Renderer>();    

    private void Start()
    {
        lrfh = GetComponent<LeftRightFromHands>();

        debugLeft = DebugObject("Debug_LeftHand", debugMatLeft, 0.015f, 0.1f);
        debugRight = DebugObject("Debug_RightHand", debugMatRight, 0.015f, 0.1f);
        debugBone = DebugObject("Debug_Center", debugMatCenter, 0.0075f, 0.1f);
        debugLeftMax = DebugObject("Debug_LeftHand_Max", debugMatMax, 0.0075f, 0.1f);
        debugRightMax = DebugObject("Debug_RightHand_Max", debugMatMax, 0.0075f, 0.1f);
        debugAverageRaw = DebugObject("Debug_Average_Raw", debugMatAverageRaw, 0.015f, 0.1f);
        debugAverageMaxLeft = DebugObject("Debug_Average_Max_Left", debugMatAverageMax, 0.0075f, 0.1f);
        debugAverageMaxRight = DebugObject("Debug_Average_Max_Right", debugMatAverageMax, 0.0075f, 0.1f);
        debugAverageSmoothed = DebugObject("Debug_Average_Smoothed", debugMatAverageSmoothed, 0.0075f, 0.15f);
    }

    private void Update()
    {
        debugLeft.transform.localPosition = new Vector3(lrfh.rawLeft, 0.1f, debugOffset);
        debugRight.transform.localPosition = new Vector3(lrfh.rawRight, 0.1f, debugOffset);
        debugBone.transform.localPosition = new Vector3(0, 0, debugOffset);
        debugLeftMax.transform.localPosition = new Vector3(lrfh.maxLeft, 0.1f, debugOffset);
        debugRightMax.transform.localPosition = new Vector3(lrfh.maxRight, 0.1f, debugOffset);

        debugAverageRaw.transform.localPosition = new Vector3(lrfh.rawAverage, -0.1f, debugOffset);
        debugAverageMaxLeft.transform.localPosition = new Vector3(lrfh.averageMaxLeft, -0.1f, debugOffset);
        debugAverageMaxRight.transform.localPosition = new Vector3(lrfh.averageMaxRight, -0.1f, debugOffset);
        debugAverageSmoothed.transform.localPosition = new Vector3(lrfh.rawAverageSmoothed, -0.1f, debugOffset);
    }
    GameObject DebugObject(string name, Material mat, float width, float diameter)
    {
        GameObject debugObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        debugObject.name = name;
        debugObject.transform.SetParent(lrfh.boneReference.parent, false);
        debugObject.transform.localScale = new Vector3(diameter, width, diameter);
        debugObject.transform.localPosition = new Vector3(0, 0, debugOffset);
        debugObject.transform.localEulerAngles = new Vector3(0, 0, 90f);

        Renderer rend = debugObject.GetComponent<Renderer>();
        rend.material = mat;
        debugObject.layer = 10;
        renderers.Add(rend);

        return debugObject;
    }

    public void Switch()
    {
        foreach(var r in renderers)
        {
            r.enabled = !debugOn;
        }
        debugOn = !debugOn;
    }
}