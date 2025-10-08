//=====================================
//         Mocap Video Player
//              Create by 
//          Vincent MEYRUEIS 
//          Jean-Francois JEGO 
//                2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MocapPlayer : MonoBehaviour {

    //Progress 
    [Range(0.0f,1.0f)]
    public float Progress = 0.0f;
    public string CurrentTime = "";
    public float ElanTime = 0.0f; 

    //OffSet
    public float VideoMocapOffSet; 

    //public float VideoActualFrame;

    //Video 
    private VideoPlayer videoPlayer;
    [Range(0.0f, 1.0f)]
    public float VideoNormalizedTime = 0.0f;
    public string VideoCurrentTime = "";
    public float  VideoDuration ;

    //Mocap Locutor 1 
    bool Mocap1Start = false;
    Animator anim1;
    AnimationClip Anim1Clip;
    public GameObject MocapObj1;
    [Range(0.0f, 1.0f)]
    public float Mocap1NormalizedTime = 0.0f;
    public string Mocap1CurrentTime = "";
    public float Mocap1Duration;
    public float Mocap1TimeScale = 1.0f;
    public float Mocap1Offset = 0.0f;

    //Mocap Locutor 2
    bool Mocap2Start = false;
    Animator anim2;
    AnimationClip Anim2Clip;
    public GameObject MocapObj2;
    [Range(0.0f, 1.0f)]
    public float Mocap2NormalizedTime = 0.0f;
    public string Mocap2CurrentTime = "";
    public float Mocap2Duration;
    public float Mocap2TimeScale = 1.0f;
    public float Mocap2Offset = 0.0f;

    //MotionCapture FrameRate
    public float MocapFrameRate = 60.6f;
    //Read Speed 
    public float MocapReadSpeed = 1.027217f;

    bool start = false;

    // Use this for initialization
    public void Start() {

        //Get Video Player 
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        //VideoDuration = (float)videoPlayer.clip.length; //DEBUG
        //videoPlayer.Prepare();
        VideoDuration = videoPlayer.frameCount / videoPlayer.frameRate;
        videoPlayer.Play(); //Start

        //Get Mocaps player 
        anim1 = MocapObj1.GetComponent<Animator>();
        anim2 = MocapObj2.GetComponent<Animator>();

        //Get Mocap Duration
        AnimationClip Clip1 = anim1.runtimeAnimatorController.animationClips[0];
        Clip1.frameRate = MocapFrameRate;
        Mocap1Duration = Clip1.length/MocapReadSpeed;
       
        AnimationClip Clip2 = anim2.runtimeAnimatorController.animationClips[0];
        Clip2.frameRate = MocapFrameRate;
        Mocap2Duration = Clip2.length/ MocapReadSpeed; 

        //MocapObj1.SetActive(false);
        //MocapObj2.SetActive(false);

        start = true;
    }
	
	// Update is called once per frame
	void Update () {

        VideoDuration = videoPlayer.frameCount / videoPlayer.frameRate;

        if (!start)
        {
            return;
        }

        //Get current Video Time 
        VideoCurrentTime = FormatSecondTime((float) videoPlayer.time);
        CurrentTime = FormatSecondTime((float)videoPlayer.time);
        VideoNormalizedTime = (float)videoPlayer.time / VideoDuration;
        ElanTime = VideoDuration * VideoNormalizedTime * 1000.0f;

        //Get current Mocap1 Time
        if (Mocap1Start)
        {
            AnimatorStateInfo State1 = anim1.GetCurrentAnimatorStateInfo(0);
            Mocap1NormalizedTime = State1.normalizedTime;
            Mocap1CurrentTime = FormatSecondTime(State1.length * State1.normalizedTime);
        }

        //Get current Mocap2 Time
        if (Mocap2Start)
        {
            AnimatorStateInfo State2 = anim2.GetCurrentAnimatorStateInfo(0);
            Mocap2NormalizedTime = State2.normalizedTime;
            Mocap2CurrentTime = FormatSecondTime(State2.length * State2.normalizedTime);
        }
 

        //Play Mocap annimation
        if (!Mocap1Start && ElanTime/1000.0f >= Mocap1Offset)
        {
            MocapObj1.SetActive(true);

            float Mocap1NormTime = GetMocapNormalizedTime(VideoNormalizedTime, VideoDuration, Mocap1Offset, Mocap1Duration);
            if (Mocap1NormTime > 0 || Mocap1NormTime < 1)
            {
                anim1.Play(currentAnimationName(anim1), 0, Mocap1NormTime);  //TODO TIME DEBUG
                Mocap1Start = true;
            }
           
        }

        if (!Mocap2Start && ElanTime / 1000.0f >= Mocap2Offset)
        {
            MocapObj2.SetActive(true);
            float Mocap2NormTime = GetMocapNormalizedTime(VideoNormalizedTime, VideoDuration, Mocap2Offset, Mocap2Duration);
            if (Mocap2NormTime > 0 || Mocap2NormTime < 1)
            {
                anim2.Play(currentAnimationName(anim2), 0, Mocap2NormTime); // TODO TIME DEBUG
                Mocap2Start = true;
            }

        }

        // Stop if Over
        if (Mocap1NormalizedTime > 1)
        {
            anim1.StopPlayback();
            //MocapObj1.SetActive(false);
            Mocap1Start = false;
        }

        if (Mocap2NormalizedTime > 1)
        {
            anim2.StopPlayback();
            //MocapObj2.SetActive(false);
            Mocap2Start = false;
        }


        //DEBUG
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpTime(Progress);
        }

    }

    string currentAnimationName(Animator anim){
        var currAnimName = "";
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips){
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;
    }

    string FormatSecondTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }

    string FormatMillisTime(float time)
    {
        time /= 1000;
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }

    public void JumpTime(float NormTime)
    {
        JumpVideoTime(NormTime);
        JumpMocapTime(NormTime);
    }

    public void Play()
    {
        MocapObj1.SetActive(true);
        MocapObj2.SetActive(true);
        videoPlayer.Play();
        JumpVideoTime(VideoNormalizedTime);
        JumpMocapTime(VideoNormalizedTime);
    }

    public void JumpVideoTime(float NormTime)
    {
        videoPlayer.frame = (long)((double)videoPlayer.frameCount * (double)NormTime);
    }

    public void JumpMocapTime(float NormTime)
    {
        //Pic Mocap1
        float Mocap1NormTime = GetMocapNormalizedTime(NormTime, VideoDuration, Mocap1Offset, Mocap1Duration);
        if (Mocap1NormTime >0 || Mocap1NormTime <1 )
            anim1.Play(currentAnimationName(anim1), 0, Mocap1NormTime);

        //Pic Mocap2
        float Mocap2NormTime = GetMocapNormalizedTime(NormTime, VideoDuration, Mocap2Offset, Mocap2Duration);
        if (Mocap2NormTime > 0 || Mocap2NormTime < 1)
            anim2.Play(currentAnimationName(anim2), 0, Mocap2NormTime);
    }

    public void Stop()
    {
        videoPlayer.Stop();

        anim1.StopPlayback();
        //MocapObj1.SetActive(false);
        Mocap2Start = false;

        anim2.StopPlayback();
        //MocapObj2.SetActive(false);
        Mocap2Start = false;
    }

    public void Resync()
    {
        JumpTime(VideoNormalizedTime);
    }

    float GetMocapNormalizedTime(float VideoNormalizedTime , float VideoDuration, float MocapOffset, float MocapDuration )
    {
        float MocapNormTime =  ((VideoNormalizedTime * VideoDuration) - MocapOffset) / MocapDuration;
        return MocapNormTime;
    }


}