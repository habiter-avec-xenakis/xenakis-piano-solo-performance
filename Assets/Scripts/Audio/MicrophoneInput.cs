using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    private AudioSource m_audioSource;
    public AudioMixerGroup m_mixerGroupMicrophone;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach(var device in Microphone.devices)
        {
            Debug.Log(device);
        }
        string m_selectedDevice = Microphone.devices[0].ToString();
        m_audioSource.outputAudioMixerGroup = m_mixerGroupMicrophone;
        m_audioSource.clip = Microphone.Start(m_selectedDevice, true, 10, AudioSettings.outputSampleRate);
        m_audioSource.Play();
    }
}
