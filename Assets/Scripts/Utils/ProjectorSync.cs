using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ProjectorSync : MonoBehaviour
{
    public Camera camSync;
    public float projScreenWidth = 1f;
    public float projScreenHeight = 1f;
    //private float _projScreenRatio;
    //public float projScreenRatio
    //{
    //    get { return _projScreenRatio; }
    //}
    public float distance = 1f;
    private float _screenRatio;
    public float screenRatio
    {
        get { return _screenRatio; }
    }

    public UIBlackStripes uiBlackStripes;
    [Range(0f, 0.5f)]
    public float lateralBlackStripes;
    //[Range(0f, 0.5f)]
    //public float topBottomBlackStripes;


    private void Start()
    {
        if(camSync)
        {
            _screenRatio = GetCameraDisplayRatio();
        }
    }

    private void OnEnable()
    {
        if (camSync)
        {
            _screenRatio = GetCameraDisplayRatio();
        }
    }

    private void Update()
    {
        if(transform.hasChanged)
        {
            SetCamera();

            transform.hasChanged = false;
        }
    }

    public float GetCameraDisplayRatio()
    {
        return (float)Display.displays[camSync.targetDisplay].systemWidth / (float)Display.displays[camSync.targetDisplay].systemHeight;
    }

    public void SetCamera()
    {
        if (camSync)
        {
            camSync.transform.position = transform.position + distance * transform.forward + Vector3.up * projScreenHeight / 2f;
            camSync.transform.rotation = transform.rotation * Quaternion.Euler(0,180,0);
            camSync.fieldOfView = 2.0f * Mathf.Atan(projScreenHeight * 0.5f / distance) * Mathf.Rad2Deg;
        }
    }

    //public void SetCameraRect()
    //{
    //    camSync.rect = new Rect(lateralBlackStripes, topBottomBlackStripes, 1f - (lateralBlackStripes * 2f), 1f - (topBottomBlackStripes * 2f));
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.localPosition + (-transform.right * projScreenWidth / 2f), transform.localPosition + transform.right * projScreenWidth / 2f);
        Gizmos.DrawLine(transform.localPosition + transform.right * projScreenWidth / 2f, transform.localPosition + transform.right * projScreenWidth / 2f + Vector3.up * projScreenHeight);
        Gizmos.DrawLine(transform.localPosition + transform.right * projScreenWidth / 2f + Vector3.up * projScreenHeight, transform.localPosition + (-transform.right * projScreenWidth / 2f) + Vector3.up * projScreenHeight);
        Gizmos.DrawLine(transform.localPosition + (-transform.right * projScreenWidth / 2f) + Vector3.up * projScreenHeight, transform.localPosition + (-transform.right * projScreenWidth / 2f));

        Gizmos.DrawIcon(transform.position, "projsync.png", true);

        if (!camSync)
        {
            return;
        }

        Vector3[] frustumCorners = new Vector3[4];
        camSync.CalculateFrustumCorners(new Rect(0, 0, 1, 1), distance, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        Gizmos.color = Color.green;
        for(int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = transform.TransformVector(frustumCorners[i]);
            Gizmos.DrawLine(worldSpaceCorner + transform.position - distance * transform.forward + Vector3.up * projScreenHeight / 2f, camSync.transform.position);
        }
    }
}
