//=====================================
//           Data Analysis
// Create by Vincent MEYRUEIS 2017
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spectrum Data Class
public class DataAnalysis {

    //Values
    public List<Vector2> Values = new List<Vector2>();

    //X Mmin Max
    public float XMin = 0;
    public float XMax = 0;

    //X Mmin Max
    public float YMin = 0;
    public float YMax = 0;


    //Nomalized Values
    //public List<Vector2> NormValues = new List<Vector2>();
    public AnimationCurve NormValues = new AnimationCurve();


    //Find Min Max
    void FindMinMax()
    {
        List<float> X = new List<float>();
        List<float> Y = new List<float>();

        //Translate Data
        for (int i = 0; i < Values.Count ; i++)
        {
            X.Add(Values[i].x);
            Y.Add(Values[i].y);
        }

        //Max
        XMax = Mathf.Max(X.ToArray());
        YMax = Mathf.Max(Y.ToArray());
        
        //Min
        XMin = Mathf.Min(X.ToArray());
        YMin = Mathf.Min(Y.ToArray());
    }

    //Compute Normalized Value
    public void NormalizedValues()
    {

        FindMinMax();


        for (int i = 0; i < Values.Count; i++)
        {
            float x = Mathf.InverseLerp(XMax, XMin, Values[i].x);
            float y = Mathf.InverseLerp(YMax, YMin, Values[i].y);
            NormValues.AddKey(x, y);
        }
    }
}

//Gesture Class
public class Gesture
{
    public string Type;
    public int ID = -1;
    public int StartFrame;
    public int StopFrame;
    public float Quality;
    public string LocutorName;
    public GameObject Locutor;
    public float Duration;

    //LeftHand
    public HandData LeftHand = new HandData();
    public HandData RightHand = new HandData();

    public Gesture()
    {
        Type = "";
        ID = -1;
        StartFrame = new int();
        StartFrame = new int();
        Quality = new int();
        LocutorName = "";
        Locutor = null;
        Duration = 0;
        LeftHand.Name = "LeftHand";
        RightHand.Name = "RightHand";
    }

    public void ComputeDuration()
    {
        float FrameTime = 0.16f;
        Duration = (StopFrame - StartFrame) * FrameTime;
    }

}

//Hand Data Class
public class HandData
{
    //Data
    public string Name = "";
    public int FrameCount = 0;
    public List<float> Times = new List<float>();
    public List<Vector3> Positions = new List<Vector3>();
    public List<Vector3> Speeds = new List<Vector3>();
    public List<Vector3> Accs = new List<Vector3>();
    public List<Vector3> Jerks = new List<Vector3>();
    public List<Quaternion> Quats = new List<Quaternion>();

    //Set Data
    public void SetData(float Time, Vector3 Position, Vector3 Speed, Vector3 Acc, Vector3 Jerk, Quaternion Quat)
    {
        Times.Add(Time);
        Positions.Add(Position);
        Speeds.Add(Speed);
        Accs.Add(Acc);
        Jerks.Add(Jerk);
        Quats.Add(Quat);
    }

    //Clear
    public void ClearData()
    {
        Times.Clear();
        Positions.Clear();
        Speeds.Clear();
        Accs.Clear();
        Jerks.Clear();
        Quats.Clear();
    }
}


