using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Indices
{
    public int indexTrack;
    public int indexSection;
    public Indices(int iTrack, int iSection)
    {
        indexTrack = iTrack;
        indexSection = iSection;
    }
}

public class MusicalTextureTrackSequencer : MonoBehaviour
{
    public MusicalTextureManager mtManager;
    public MusicalTextureTracksScriptableObject mtTrackScrObj;

    public TextMeshProUGUI textCurrentSequence;
    public Renderer sketchesRenderer;
    private float sketchesRendererScaleBase;

    private int currentIndex;
    private Indices[] indices;

    public UnityEventFloat onSilenceBegin = new UnityEventFloat();
    public UnityEvent onSilenceEnd = new UnityEvent();
    public UnityEventInt onIndexChange = new UnityEventInt();

    private void Awake()
    {
        currentIndex = 0;

        List<Indices>  indicesList = new List<Indices>();

        for(int t = 0; t < mtTrackScrObj.mtTracks.Count; t++)
        {
            for(int s = 0; s < mtTrackScrObj.mtTracks[t].trackSections.Count; s++)
            {
                indicesList.Add(new Indices(t, s));
            }
        }

        indices = indicesList.ToArray();
        Debug.Log("Indices list length: " + indices.Length);
    }

    private void Start()
    {
        if(sketchesRenderer != null)
        {
            sketchesRendererScaleBase = sketchesRenderer.transform.localScale.x;
            //sketchesRenderer.enabled = false;
        }
        SetSequenceIndex(currentIndex);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            PreviousSection();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextSection();
        }
    }

    public void NextSection()
    {
        currentIndex++;
        if (currentIndex > indices.Length - 1)
        {
            currentIndex = indices.Length - 1;
        }
        SetSequenceIndex(currentIndex);
    }

    public void PreviousSection()
    {
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = 0;
        }
        SetSequenceIndex(currentIndex);
    }


    public void SetSequenceIndex(int index)
    {
        //Debug.Log("SetSequenceIndex");
        currentIndex = index;
        MusicalTextureTrackSection mtTrackSection = mtTrackScrObj.mtTracks[indices[index].indexTrack].trackSections[indices[index].indexSection];
        onIndexChange.Invoke(index);
        string textSequence = "";

        if (mtTrackScrObj.mtTracks[indices[index].indexTrack].isSilent)
        {
            textSequence = "Silence";
            if(sketchesRenderer)
            {
                sketchesRenderer.enabled = false;
            }

            mtManager.MusicalTexturesEnable(false);

            onSilenceBegin.Invoke(mtTrackScrObj.mtTracks[indices[index].indexTrack].silenceDuration);
        }
        else
        {
            onSilenceEnd.Invoke();
        }

        textSequence = index.ToString("00") + " | ";
        if(mtTrackScrObj.mtTracks[indices[index].indexTrack].isSilent)
        {
            textSequence += "[Silence]";
        }
        else
        {
            textSequence += mtManager.musicalTexturesData.musicalTextures[mtTrackSection.mtMainIndex].textureName;
            if (mtTrackSection.trackSectionType == TrackSectionType.Dual)
            {
                textSequence += " & " + mtManager.musicalTexturesData.musicalTextures[mtTrackSection.mtSecondaryIndex].textureName;
            }
        }

        textCurrentSequence.text = textSequence;

        switch(mtTrackSection.trackSectionType)
        {
            case (TrackSectionType.Single):
                mtManager.SetMusicalTexture(mtTrackSection.mtMainIndex);
                break;
            case (TrackSectionType.Dual):
                mtManager.SetMusicalTextures(mtTrackSection.mtMainIndex, mtTrackSection.mtSecondaryIndex);
            break;
        }

        if (mtTrackScrObj.mtTracks[indices[index].indexTrack].sketch != null && sketchesRenderer != null)
        {
            sketchesRenderer.enabled = true;
            Texture2D sketch = mtTrackScrObj.mtTracks[indices[index].indexTrack].sketch;
            sketchesRenderer.material.SetTexture("mtSketch", sketch);
            float ratio = (float)sketch.width / (float)sketch.height;

            float scale = sketchesRendererScaleBase;
            if (ratio > 1)
            {
                scale /= ratio / 2;
            }
            else if (ratio < 1)
            {
                scale /= ratio * 2;
            }

            sketchesRenderer.transform.localScale = new Vector3(scale * ratio, scale, 1);
        }
        else
        {
            sketchesRenderer.enabled = false;
        }
    }
}
