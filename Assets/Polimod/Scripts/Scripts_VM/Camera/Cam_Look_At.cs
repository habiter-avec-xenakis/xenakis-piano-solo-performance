using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Look_At : MonoBehaviour { 

    public GameObject Obj = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Obj != null) {
            transform.LookAt(Obj.transform, Vector3.up);
        }

            
    }
}
