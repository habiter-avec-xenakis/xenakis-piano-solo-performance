using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDropDown : MonoBehaviour
{

    public Dropdown CamerasDrop;
    public Camera[] Cameras = new Camera[4];
    public List<string> CamerasName;

    // Start is called before the first frame update
    void Start()
    {
        CamerasDrop = gameObject.GetComponent<Dropdown>();

        for (int i = 0; i < Cameras.Length; i++)
        {
            CamerasName.Add(Cameras[i].name);
            Cameras[i].enabled = false;
        }

        CamerasDrop.AddOptions(CamerasName);
        
        SelectCam();
    }

    // Update is called once per frame
    public void SelectCam()
    {

        for (int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].enabled = false;
        }
        Cameras[CamerasDrop.value].enabled = true;
    }
}
