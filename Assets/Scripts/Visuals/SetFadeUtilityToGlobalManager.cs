using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Kino.PostProcessing;

public class SetFadeUtilityToGlobalManager : MonoBehaviour
{
    public Volume volume;
    private Utility utility;

    private void Start()
    {
        volume.profile.TryGet<Utility>(out utility);
        PerformanceGlobalManager.volumeUtilityFade = utility;
    }
}