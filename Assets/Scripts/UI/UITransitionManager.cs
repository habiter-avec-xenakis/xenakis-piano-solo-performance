using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITransitionManager : MonoBehaviour
{
    public GameObject controlsTransitionMists;
    public GameObject controlsTransitionEvryali;
    public GameObject avatarRig;
    public RawImage avatarTransition;
    private Material avatarTransitionMat;

    private void Start()
    {
        avatarTransitionMat = new Material(avatarTransition.material);
        avatarTransition.material = avatarTransitionMat;
    }

    public void ActivateMistsControls()
    {
        controlsTransitionMists.SetActive(true);
    }

    public void ActivateEvryaliControls()
    {
        controlsTransitionEvryali.SetActive(true);
        avatarRig.SetActive(true);
    }

    public void SetAvatarTransitionOpacity(float value)
    {
        avatarTransition.material.SetFloat("_Alpha", value);
    }
}