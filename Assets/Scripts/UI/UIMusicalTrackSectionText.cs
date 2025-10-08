using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIMusicalTrackSectionText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public MusicalTextureTrackSequencer mtTrackSequencer;
    private MusicalTextureTracksScriptableObject mtTrackScrObj;

    private int[] trackNumbers;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        mtTrackScrObj = mtTrackSequencer.mtTrackScrObj;

        int trackNumber = 1;

        trackNumbers = new int[mtTrackScrObj.mtTracks.Count];
        for (int t = 0; t < mtTrackScrObj.mtTracks.Count; t++)
        {

            var mtTrack = mtTrackSequencer.mtTrackScrObj.mtTracks[t];
            if (mtTrack.isSilent)
            {
                trackNumbers[t] = -1;
            }
            else
            {
                trackNumbers[t] = trackNumber;
                trackNumber++;
            }
        }

        //for(int i = 0; i < trackNumbers.Length;i++)
        //{
        //    Debug.Log((i + 1) + " | " + trackNumbers[i]);
        //}
    }

    public void SetText(int index)
    {
        //Debug.Log("SetText(" + index + ")");

        if(trackNumbers[index] == -1)
        {
            text.text = "Silence [" + index.ToString("00") +"]";
        }
        else
        {
            text.text = "Track " + trackNumbers[index].ToString("00");
        }
    }
}