using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Main")]
    public AudioSource audioSource;
    private AudioClip audioClip;
    [Range(0f, 1f)]
    public float volumeMultiplier = 1f;

    [Header("Paths")]
    public string audioName = "";
    public bool useDataPath = true;
    public bool autoPlay = false;
    public string soundPath;

    private void Awake()
    {
        if(autoPlay)
        {
            StartCoroutine(LoadAudio());
        }
    }

    public void SetSoundFile(string fileName)
    {
        audioName = fileName;
        useDataPath = true;
        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio()
    {
        string path;
        if(useDataPath)
        {
            path = Application.persistentDataPath + soundPath;
        }
        else
        {
            path = soundPath;
        }
        //Debug.Log(path);

        WWW request = GetAudioFromFile(path, audioName);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value * volumeMultiplier;
    }
}