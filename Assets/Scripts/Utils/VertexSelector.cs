using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class VectorSelectorElement
{
    public GameObject gizmoObject;
    public GameObject uiObject;
    public int vertexIndex;
    public Vector3 vertexPosition;
}

public class VertexSelector : MonoBehaviour
{
    public Collider coll;
    public Transform camRig;
    public Camera cam;
    public Transform uiParent;
    public GameObject uiPrefab;

    // Gizmos
    public Material gizmoMaterial;
    public Material gizmoMirrorMaterial;
    public Material gizmoSelectedMaterial;
    private Transform gizmo;
    private Transform gizmoMirror;
    private Renderer gizmoRend;
    private Renderer gizmoMirrorRend;

    private float zoomCurrent = 0f;
    public float fovMin = 17f;
    public float fovMax = 64f;
    private float verticalCurrent = 0f;
    private float verticalPos;
    public float verticalExtents = 0.15f;

    private bool cursorActive = false;
    private bool mirrorActive = true;
    private bool mirrorBlock = false;

    private int currentVertexIndex = -1;
    private Vector3 currentVertexPosition;
    private int currentVertexMirrorIndex = -1;
    private Vector3 currentVertexMirrorPosition;

    public List<VectorSelectorElement> vectorSelectorElements = new List<VectorSelectorElement>();

    private void Start()
    {
        // Gizmos setup
        var gizmoObject = Gizmo("VertexSelector_Gizmo", gizmoMaterial);
        gizmo = gizmoObject.transform;
        gizmoRend = gizmoObject.GetComponent<Renderer>();

        var gizmoMirrorObject = Gizmo("VertexSelector_Gizmo_Mirror", gizmoMirrorMaterial);
        gizmoMirror = gizmoMirrorObject.transform;
        gizmoMirrorRend = gizmoMirrorObject.GetComponent<Renderer>();

        verticalPos = camRig.position.y;
    }

    private GameObject Gizmo(string name, Material material)
    {
        GameObject gizmoObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gizmoObject.name = name;
        var sphereCollider = gizmoObject.GetComponent<SphereCollider>();
        Destroy(sphereCollider);

        gizmoObject.transform.localScale = Vector3.one * 0.0025f;
        var rend = gizmoObject.GetComponent<Renderer>();
        rend.material = material;

        return gizmoObject;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && cursorActive)
        {
            if(currentVertexIndex > -1)
            {
                AddElement(currentVertexIndex, currentVertexPosition);
                if (currentVertexMirrorIndex > -1 && !mirrorBlock)
                {
                    AddElement(currentVertexMirrorIndex, currentVertexMirrorPosition);
                }
            }
        }
    }
    
    private void AddElement(int index, Vector3 position)
    {
        VectorSelectorElement element = new VectorSelectorElement();
        GameObject gizmoObject = Gizmo("VertexSelector_Gizmo_Seleted_" + index, gizmoSelectedMaterial);
        gizmoObject.transform.position = position;
        element.gizmoObject = gizmoObject;
        element.vertexIndex = index;
        element.vertexPosition = position;

        GameObject uiElementInstance = Instantiate(uiPrefab);
        element.uiObject = uiElementInstance;
        uiElementInstance.transform.SetParent(uiParent, false);

        var btn = uiElementInstance.GetComponentInChildren<Button>();
        btn.onClick.AddListener(delegate { DeleteElement(vectorSelectorElements.Count - 1); });

        var txt = uiElementInstance.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = index.ToString("000000") + " | x " + position.x.ToString("0.00") + " y " + position.y.ToString("0.00") + " z " + position.z.ToString("0.00");

        vectorSelectorElements.Add(element);
    }

    private void DeleteElement(int index)
    {
        Destroy(vectorSelectorElements[index].uiObject);
        Destroy(vectorSelectorElements[index].gizmoObject);
        vectorSelectorElements.RemoveAt(index);
    }

    private void FixedUpdate()
    {
        if (!cursorActive)
        {
            return;
        }

        Ray rayCam = cam.ScreenPointToRay(Input.mousePosition);
        RaycastVertexPosition(rayCam, gizmo, gizmoRend, out currentVertexIndex, out currentVertexPosition);

        gizmo.transform.position = currentVertexPosition;

        gizmoMirrorRend.enabled = false;

        if(Mathf.Abs(currentVertexPosition.x) < 0.003f)
        {
            mirrorBlock = true;
        }
        else
        {
            mirrorBlock = false;
        }

        if(mirrorActive && !mirrorBlock)
        {
            Ray rayMirror = new Ray(Vector3MirrorX(rayCam.origin), Vector3.Normalize(Vector3MirrorX(currentVertexPosition) - Vector3MirrorX(cam.transform.position)));
            Debug.DrawLine(Vector3MirrorX(cam.transform.position), Vector3MirrorX(currentVertexPosition), Color.red);

            RaycastVertexPosition(rayMirror, gizmoMirror, gizmoMirrorRend, out currentVertexMirrorIndex, out currentVertexMirrorPosition);
            gizmoMirror.transform.position = currentVertexMirrorPosition;
        }

        //Debug.Log(currentVertexIndex + " | " + currentVertexMirrorIndex);
    }

    private void RaycastVertexPosition(Ray ray, Transform gizmo, Renderer gizmoRend, out int vertexIndex, out Vector3 vertexPosition)
    {
        vertexIndex = 0;
        vertexPosition = Vector3.zero;

        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
        {
            currentVertexIndex = -1;
            currentVertexMirrorIndex = -1;
            gizmoRend.enabled = false;
            return;
        }

        gizmoRend.enabled = true;

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        int i0 = triangles[hit.triangleIndex * 3 + 0];
        int i1 = triangles[hit.triangleIndex * 3 + 1];
        int i2 = triangles[hit.triangleIndex * 3 + 2];

        Vector3 p0 = vertices[i0];
        Vector3 p1 = vertices[i1];
        Vector3 p2 = vertices[i2];

        Transform hitTransform = hit.collider.transform;

        p0 = hitTransform.TransformPoint(p0);
        p1 = hitTransform.TransformPoint(p1);
        p2 = hitTransform.TransformPoint(p2);

        Debug.DrawLine(p0, p1, Color.green);
        Debug.DrawLine(p1, p2, Color.green);
        Debug.DrawLine(p2, p0, Color.green);

        vertexPosition = GetClosestPoint(hit.point, p0, p1, p2, i0, i1, i2, out vertexIndex);
        Debug.DrawRay(vertexPosition, meshCollider.sharedMesh.normals[vertexIndex] * 0.05f);
    }

    private Vector3 GetClosestPoint(Vector3 hitPos, Vector3 p0, Vector3 p1, Vector3 p2, int i0, int i1, int i2, out int closestIndex)
    {
        Vector3 closestPoint = p0;
        closestIndex = i0;

        float d0 = Vector3.Distance(hitPos, p0);
        float d1 = Vector3.Distance(hitPos, p1);
        float d2 = Vector3.Distance(hitPos, p2);

        if(d1 < d0)
        {
            closestPoint = p1;
            closestIndex = i1;
        }
        if (d2 < d1)
        {
            closestPoint = p2;
            closestIndex = i2;
        }

        return closestPoint;
    }

    public void SetCamRigRotation(float angle)
    {
        camRig.localEulerAngles = new Vector3(camRig.localEulerAngles.x, angle, camRig.localEulerAngles.z);
    }

    public void SetCamZoom(float value)
    {
        zoomCurrent = value;
        float fov = fovMax - (fovMax - fovMin) * value;
        cam.fieldOfView = fov;
        SetVerticalMovement(verticalCurrent);
    }

    public void SetVerticalMovement(float value)
    {
        verticalCurrent = value;
        camRig.position = new Vector3(camRig.position.x, verticalPos + verticalExtents * value * zoomCurrent, camRig.position.z);
    }

    public void SetCursorActive(bool value)
    {
        cursorActive = value;
        if(!value)
        {
            gizmoRend.enabled = false;
        }
    }

    public void SetMirrorMode(bool value)
    {
        mirrorActive = value;
    }

    private Vector3 Vector3MirrorX(Vector3 v3)
    {
        return new Vector3(-v3.x, v3.y, v3.z);
    }

    public void WriteTextFile()
    {
        string path = Application.dataPath + "/Data/VertexSelector/VertexSelector.txt";
        StreamWriter sw = new StreamWriter(path, false);
        string content = "";
        foreach(var element in vectorSelectorElements)
        {
            content += element.vertexIndex + ";";
        }
        sw.WriteLine(content);
        sw.Close();
    }
}