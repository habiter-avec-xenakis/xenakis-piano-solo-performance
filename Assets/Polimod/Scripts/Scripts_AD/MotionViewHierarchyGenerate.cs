 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionViewHierarchyGenerate : MonoBehaviour
{
    public Transform targetParent;
    public Material linesMaterial;

    [Range(0.001f, 0.5f)]
    public float trailLinesWidth = 0.025f;
    [Range(0f, 10f)]
    public float scaleSpeed = 1f;
    [Range(0f, 10f)]
    public float scaleAcc = 1f;
    [Range(0f, 10f)]
    public float scaleJerk = 1f;

    public Color colorPosition = Color.black;
    public Color colorSpeed = Color.red;
    public Color colorAcc = Color.green;
    public Color colorJerk = Color.blue;

    [Range(120, 1200)]
    public int trailLenght = 120;
    [Range(1,120)]
    public int movingAverageSample = 15;

    private List<MotionView> motionViews = new List<MotionView>();

    private void Start()
    {
        var children = targetParent.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            if(child.name == targetParent.name)
            {
                continue;
            }

            GameObject mvObject = new GameObject();
            mvObject.name = "MotionView_" + child.name;
            mvObject.transform.parent = this.transform;

            MotionView mv = mvObject.AddComponent<MotionView>();
            mv.MotionObj = child.gameObject;
            mv.LinesMaterials = linesMaterial;
            mv.Vector = false;

            mv.SpeedActive = true;
            mv.AccActive = true;
            mv.JerkActive = true;

            mv.Pos_Col = colorPosition;
            mv.Speed_Col = colorSpeed;
            mv.Acc_Col = colorAcc;
            mv.Jerk_Col = colorJerk;

            motionViews.Add(mv);
        }

    }

    private void Update()
    {
        foreach(var mv in motionViews)
        {
            mv.TrailLinesWidth = trailLinesWidth;

            mv.GizmoSpeedScale = scaleSpeed;
            mv.GizmoAccScale = scaleAcc;
            mv.GizmoJerkScale = scaleJerk;

            mv.MovingAverageSample = 120;
        }
    }
}
