using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HermaRigPreset
{
    public Vector2 offset;
    public float scale;
    public float angle;
}

public class HermaRigControl : MonoBehaviour
{
    private HermaRigPreset lastPreset;
    private Transform patternTransform;

    public int startingIndex;
    private bool applyingPreset = false;
    private bool lastApplyingPreset = false;
    private int currentIndex = 0;
    private float time = 0f;
    public float speed = 2f;
    public AnimationCurve curve;
    private float targetAngle;

    public HermaRigPreset[] rigPresets;

    public UnityEvent onBeginApplyingPreset;
    public UnityEvent onStopApplyingPreset;

    private void Awake()
    {
        // Get child transform
        patternTransform = transform.GetChild(0);

        // Init
        lastPreset = new HermaRigPreset();
        currentIndex = startingIndex;
        SetValues(1f);
    }

    private void Update()
    {
        if(applyingPreset)
        {
            time += Time.deltaTime * speed;

            SetValues(curve.Evaluate(time));

            if(time > 1f)
            {
                applyingPreset = false;
                time = 0f;
            }
        }

        if(lastApplyingPreset != applyingPreset)
        {
            if(applyingPreset && onBeginApplyingPreset != null)
            {
                onBeginApplyingPreset.Invoke();
                //Debug.Log("onBeginApplyingPreset");
            }
            else if (!applyingPreset && onStopApplyingPreset != null)
            {
                onStopApplyingPreset.Invoke();
                //Debug.Log("onStopApplyingPreset");
            }
        }
        lastApplyingPreset = applyingPreset;
    }

    public void ApplyPreset(int index)
    {
        if(applyingPreset || index == currentIndex)
        {
            return;
        }

        lastPreset = rigPresets[currentIndex];
        //Debug.Log("[HermaRigControl] ApplyPreset(" + index + ")");
        applyingPreset = true;
        currentIndex = index;
    }

    private void SetValues(float progression)
    {
        transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(lastPreset.angle, rigPresets[currentIndex].angle, progression), 0f);
        patternTransform.transform.localScale = Vector3.one * Mathf.Lerp(lastPreset.scale, rigPresets[currentIndex].scale, progression);
        patternTransform.transform.localPosition = new Vector3(Mathf.Lerp(lastPreset.offset.x, rigPresets[currentIndex].offset.x, progression), Mathf.Lerp(lastPreset.offset.y, rigPresets[currentIndex].offset.y, progression), patternTransform.transform.localPosition.z);
    }
}