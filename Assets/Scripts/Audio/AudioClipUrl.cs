using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioClipUrl : MonoBehaviour
{
    public string path;
    private AudioSource audioSource;
    private AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator GetAudioClip(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.AIFF))
        {
            yield return www.SendWebRequest();

            audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
        }

        audioSource.Play();
    }
}
