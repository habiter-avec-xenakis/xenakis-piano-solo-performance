//=====================================
//           Motion Tracker
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTracker : MonoBehaviour {


    Vector3 Position;
    public float Threshold = 0.01f;

    [Range(0.0f, 10.0f)]
    public float Size = 1.0f;


    // Use this for initialization
    void Start () {
        Position = transform.position;
        transform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 Dist = transform.position - Position;


        if (Dist.magnitude >= Threshold)
        {
            transform.localScale = Vector3.one * Size;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
        Position = transform.position;
    }
}
