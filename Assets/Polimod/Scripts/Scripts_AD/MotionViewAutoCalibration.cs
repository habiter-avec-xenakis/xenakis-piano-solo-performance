using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MotionView))]
public class MotionViewAutoCalibration : MonoBehaviour
{
    private MotionView motionView;
    private float m_maxSpeedMag = 0f;
    private float m_speedMagCal = 0f;

    public Transform debugCurrent;
    public Transform debugSpeedMag;
    public Transform debugSpeedCal;

    private void Awake()
    {
        motionView = GetComponent<MotionView>();
    }

    private void Update()
    {
        debugCurrent.transform.position = new Vector3(debugCurrent.transform.position.x, motionView.CurrentSpeedMag, debugCurrent.transform.position.z);
        debugSpeedMag.transform.position = new Vector3(debugSpeedMag.transform.position.x, m_maxSpeedMag, debugSpeedMag.transform.position.z);
        debugSpeedCal.transform.position = new Vector3(debugSpeedCal.transform.position.x, m_speedMagCal, debugSpeedCal.transform.position.z);

        if(motionView.CurrentSpeedMag > m_maxSpeedMag)
        {
            m_maxSpeedMag = motionView.CurrentSpeedMag;
        }
        m_maxSpeedMag -= Time.deltaTime * 0.1f;
        m_speedMagCal = motionView.CurrentSpeedMag / m_maxSpeedMag;
        motionView.GizmoSpeedScale = m_speedMagCal * 10f;
    }
}
