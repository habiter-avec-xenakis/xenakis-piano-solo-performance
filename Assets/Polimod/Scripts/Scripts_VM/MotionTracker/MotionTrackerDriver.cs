using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTrackerDriver : MonoBehaviour {

    public List<GameObject> Trackers;

    [Range(0.0f,10.0f)]
    public float Size = 1.0f;
    public float Threshold = 0.01f;

	// Use this for initialization
	void Start () {

        
        for (int i = 0; i < Trackers.Count; i++)
        {
            MotionTracker M = Trackers[i].GetComponent<MotionTracker>();
            if (!M)
            {
                M = Trackers[i].AddComponent<MotionTracker>();
                M.Threshold = Threshold;
                M.Size = Size;
            }

        }

    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < Trackers.Count; i++)
        {
            MotionTracker M = Trackers[i].GetComponent<MotionTracker>();
            M.Threshold = Threshold;
            M.Size = Size;
        }


    }

















}
