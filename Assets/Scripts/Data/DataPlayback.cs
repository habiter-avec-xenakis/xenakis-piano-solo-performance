using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class DataPlayback : MonoBehaviour
{
    public DataSet dataSetCurrent;

    // Video
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;

    private bool videoPlayer1Ready = false;
    private bool videoPlayer2Ready = false;

    // Audio
    public AudioSource audioSource;
    private AudioClip audioClip;
    public TimelineAsset timeline;
    public PlayableDirector playableDirector;

    private bool audioClipReady = false;

    public UnityEvent audioClipIsReady = new UnityEvent();

    // Playback
    public UnityEvent dataIsReady = new UnityEvent();
    public UnityEvent isPlaying = new UnityEvent();
    public UnityEvent isStopping = new UnityEvent();


    public void Play()
    {
        PlayAll();
    }

    public void Stop()
    {
        StopAll();
    }

    void Prepared(UnityEngine.Video.VideoPlayer vPlayer)
    {
        Debug.Log("Prepared " + vPlayer.name);

        if (vPlayer == videoPlayer1)
        {
            videoPlayer1Ready = true;
        }

        if (vPlayer == videoPlayer2)
        {
            videoPlayer2Ready = true;
        }

        CheckIfReady();
    }

    IEnumerator GetAudioClip(string path)
    {
        Debug.Log("GetAudioClip " + path);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.AIFF))
        {
            yield return www.SendWebRequest();

            /*if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {*/
            audioClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = audioClip;
            audioClipReady = true;
            audioClipIsReady.Invoke();
            CheckIfReady();
            //}
        }
    }

    private void CheckIfReady()
    {
        Debug.Log("CheckIfReady()");

        if(videoPlayer1Ready == true && videoPlayer2Ready == true && audioClipReady == true)
        {
            dataIsReady.Invoke();
            videoPlayer1.prepareCompleted -= Prepared;
            videoPlayer2.prepareCompleted -= Prepared;
        }
    }

    private void PlayAll()
    {
        isPlaying.Invoke();

        videoPlayer1.Stop();
        videoPlayer1.Play();

        videoPlayer2.Stop();
        videoPlayer2.Play();

        audioSource.Play();
        playableDirector.Play();

        StartCoroutine(CheckTime());
    }

    private void StopAll()
    {
        isStopping.Invoke();
        videoPlayer1.Stop();
        videoPlayer2.Stop();
        audioSource.Stop();
        playableDirector.Stop();
        audioSource.clip = null;
    }

    public void SetDataSet(DataSet dataSet)
    {
        Debug.Log("SetDataSet " + dataSet.pathAudio);

        StopAll();

        videoPlayer1Ready = false;
        videoPlayer2Ready = false;
        audioClipReady = false;

        dataSetCurrent = dataSet;

        // ! Very dirty, to change !
        if (dataSet.pathsVideo.Length > 0)
        {
            videoPlayer1.url = dataSet.pathsVideo[0];
            videoPlayer1.prepareCompleted += Prepared;
            videoPlayer1.Prepare();

            if (dataSet.pathsVideo.Length == 2)
            {
                videoPlayer2.url = dataSet.pathsVideo[1];
                videoPlayer2.prepareCompleted += Prepared;
                videoPlayer2.Prepare();
            }
        }
        if (dataSet.pathAudio != null)
        {
            string soundPath = "file://" + dataSet.pathAudio;
            StartCoroutine(GetAudioClip(soundPath));
        }
    }

    IEnumerator CheckTime()
    {
        yield return new WaitForEndOfFrame();
        videoPlayer1.time = 0f;
        videoPlayer2.time = 0f;
        audioSource.time = 0f;
        playableDirector.time = 0f;
    }
}