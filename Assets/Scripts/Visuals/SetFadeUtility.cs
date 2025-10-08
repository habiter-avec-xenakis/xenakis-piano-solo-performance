using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Kino.PostProcessing;

public class SetFadeUtility : MonoBehaviour
{
    private Utility localUtility;
    public bool setToBlackOnStart = true;

    [Range(0f,1f)]
    public float fade = 1f;
    private float lastFade;

    private void Start()
    {
        lastFade = fade;

        if (!PerformanceGlobalManager.volumeUtilityFade)
        {
            GameObject volumeObject = new GameObject();
            volumeObject.name = "Volume_UtilityFade";
            Volume volume = volumeObject.AddComponent<Volume>();
            volume.isGlobal = true;
            VolumeProfile volumeProfile = ScriptableObject.CreateInstance<VolumeProfile>();
            volume.profile = volumeProfile;
            localUtility = volumeProfile.Add<Utility>();
            localUtility.fade.overrideState = true;
        }

        if(setToBlackOnStart)
        {
            SetFadeAlpha(1f);
        }
    }

    private void Update()
    {
        if(lastFade != fade)
        {
            SetFadeAlpha(fade);
        }
        lastFade = fade;
    }

    public void SetFadeAlpha(float value)
    {
        //Debug.Log("SetFadeAlpha(" + value.ToString("0.000") + ") " + Time.time);

        Color color = new Color(0, 0, 0, value);
        if (PerformanceGlobalManager.volumeUtilityFade)
        {
            PerformanceGlobalManager.volumeUtilityFade.fade.value = color;
        }
        else
        {
            localUtility.fade.value = color;
        }

        fade = value;
        lastFade = value;
    }
}