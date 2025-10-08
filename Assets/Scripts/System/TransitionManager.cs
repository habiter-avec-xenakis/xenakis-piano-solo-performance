using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TransitionManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public float[] initialTimes;
    private float counter;
    private float maxTime = -1f;
    private bool counting = true;

    public UnityEvent onTransitionToAR;
    public UnityEvent onTransitionToHerma;
    public UnityEvent onTransitionToMists;
    public UnityEvent onTransitionToEvryali;
    public UnityEvent onTransitionToAirAR;

    private void Awake()
    {
        //Debug.Log("YOP" + PerformanceGlobalManager.transitionIndex);

        var index = PerformanceGlobalManager.transitionIndex;

        counter = initialTimes[index];

        if(PerformanceGlobalManager.transitionIndex < initialTimes.Length - 1)
        {
            maxTime = initialTimes[index + 1];
        }

        //Debug.Log("Index: " + index + " | Initial: " + initialTimes[index] + " | Max: " + maxTime);

        playableDirector.time = initialTimes[index];
        playableDirector.Play();

        switch(PerformanceGlobalManager.transitionIndex)
        {
            case (0):
                if (onTransitionToAR != null)
                {
                    onTransitionToAR.Invoke();
                }
                break;
            case (1):
                if (onTransitionToHerma != null)
                {
                    onTransitionToHerma.Invoke();
                }
                break;
            case (2):
                if(onTransitionToMists != null)
                {
                    onTransitionToMists.Invoke();
                }
                break;
            case (3):
                if(onTransitionToEvryali != null)
                {
                    onTransitionToEvryali.Invoke();
                }
                break;
            case (4):
                if (onTransitionToAirAR != null)
                {
                    onTransitionToAirAR.Invoke();
                }
                break;
        }
    }

    private void Update()
    {
        if(counting)
        {
            counter += Time.deltaTime;
            if (maxTime > -1f && counter > maxTime)
            {
                //Debug.Log("Timeline stop.");
                playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                counting = false;
            }
        }
    }
}
