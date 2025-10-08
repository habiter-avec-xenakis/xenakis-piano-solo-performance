using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicalTextureTargetDebug : MonoBehaviour
{
    private MusicalTextureManager mtManager;

    public Image imageMain;
    public Image imageSecondary;
    public Image imageSecondaryCross;

    public TextMeshProUGUI textTransition;
    public TextMeshProUGUI textIsOn;
    public TextMeshProUGUI trackSectionMode;

    private void Start()
    {
        mtManager = FindObjectOfType<MusicalTextureManager>();
    }

    private void Update()
    {
        imageMain.color = mtManager.mtDataMain.mtCurrent.color;
        imageSecondary.color = mtManager.mtDataSecondary.mtCurrent.color;
        textTransition.text = "transition: " +  mtManager.transitionValue;
        textIsOn.text = "isOn: " + mtManager.isOn;
        trackSectionMode.text = "Track section mode: " + mtManager.trackSectionType;

        switch(mtManager.trackSectionType)
        {
            case (TrackSectionType.Single):
                imageSecondary.enabled = false;
                imageSecondaryCross.enabled = true;
                break;

            case (TrackSectionType.Dual):
                imageSecondary.enabled = true;
                imageSecondaryCross.enabled = false;
                break;
        }
    }
}