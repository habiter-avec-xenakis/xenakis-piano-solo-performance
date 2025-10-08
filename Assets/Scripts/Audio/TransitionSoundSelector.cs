using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionSound
{
    public string fileName;
    public float volumeMultiplier;
}

public class TransitionSoundSelector : MonoBehaviour
{
    public AudioController audioController;
    //public string[] fileNames = new string[5];
    public TransitionSound[] transitionSounds = new TransitionSound[5];

    private void Awake()
    {
        audioController.volumeMultiplier = transitionSounds[PerformanceGlobalManager.transitionIndex].volumeMultiplier;
        audioController.SetSoundFile(transitionSounds[PerformanceGlobalManager.transitionIndex].fileName);
    }
}