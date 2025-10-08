using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MistsPatternManager : MonoBehaviour
{
    public VisualEffect caveVfx;
    public GameObject patternPrefab;

    public void InvokePattern(int index)
    {
        var patternInstance = Instantiate(patternPrefab);
        patternInstance.transform.position = new Vector3(0f, 0f, caveVfx.GetFloat("Cave Length"));

        var patternAnimator = patternInstance.AddComponent<MistsPatternAnimator>();
        patternAnimator.speed = caveVfx.GetFloat("Speed");
        patternAnimator.killDistance = -caveVfx.GetFloat("Killbox Offset");

        var patternControl = patternPrefab.GetComponent<MistsPatternControl>();
        if(patternControl != null)
        {
            index = Mathf.Clamp(index, 0, patternControl.mistsPatternParameters.Length - 1);
            patternControl.patternRenderer = patternInstance.GetComponent<Renderer>();
            patternControl.SetPattern(index);
        }

    }
}