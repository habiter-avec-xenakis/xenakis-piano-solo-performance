using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MotionViewVfx : MonoBehaviour
{
    public MotionView motionView;
    public VisualEffect vfx;

    private void Update()
    {
        vfx.transform.position = motionView.MotionObj.transform.position;

        // Speed
        vfx.SetVector3("Speed", motionView.CurrentSpeed);
        vfx.SetFloat("SpeedScale", motionView.GizmoSpeedScale);

        // Acceleration
        vfx.SetVector3("Acceleration", motionView.CurrentAcc);
        vfx.SetFloat("AccelerationScale", motionView.GizmoAccScale);

        // Jerk
        vfx.SetVector3("Jerk", motionView.CurrentJerk);
        vfx.SetFloat("JerkScale", motionView.GizmoJerkScale);
    }
}
