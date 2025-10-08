using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageWebcam : MonoBehaviour
{
    private RawImage rawImage;
    //public string webcamName = "USB_Camera";
    //public RawImage controlsRepeater;
    //private WebCamTexture webcamTexture;
    private Material webCamMaterial;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        webCamMaterial = new Material(rawImage.material);
        rawImage.material = webCamMaterial;

        //    foreach (var d in WebCamTexture.devices)
        //    {
        //        //Debug.Log(d.name);

        //        if(d.name == webcamName)
        //        {
        //            webcamTexture = new WebCamTexture(webcamName);

        //            rawImage.texture = webcamTexture;

        //            if (controlsRepeater)
        //            {
        //                controlsRepeater.texture = webcamTexture;
        //            }

        //            webcamTexture.Play();
        //        }
        //    }

        //    PerformanceGlobalManager.onPreSceneChange.AddListener(DisableWebcam);
    }

    public void SetAlpha(float value)
    {
        rawImage.material.SetFloat("_Alpha", value);
    }

    //private void DisableWebcam()
    //{
    //    if(webcamTexture)
    //    {
    //        webcamTexture.Stop();
    //    }
    //}
}