using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ColorTools;

public class UITrackSelectionButtons : MonoBehaviour
{
    private MusicalTextureTrackSequencer mtTrackSequencer;
    private ToggleGroup toggleGroup;
    private RectTransform rectTransform;

    public GameObject prefabTrackNumber;
    public GameObject prefabTrackSection;
    public GameObject prefabTrackSilence;
    public GameObject prefabMusicalTexture;
    public GameObject prefabSpace;

    public Scrollbar scrollBar;

    //private List<GameObject> currentPlayObjects = new List<GameObject>();
    private List<Image> currentPlayImages = new List<Image>();

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mtTrackSequencer = FindObjectOfType<MusicalTextureTrackSequencer>();
        mtTrackSequencer.onIndexChange.AddListener(SetPlayCurrent);
        toggleGroup = GetComponent<ToggleGroup>();
        CreateButtons();
    }

    public void CreateButtons()
    {
        ClearTracks();
        InstantiateButtons();
    }

    private void InstantiateButtons()
    {
        int trackNumber = 1;
        int trackIndex = 0;

        for(int t = 0; t < mtTrackSequencer.mtTrackScrObj.mtTracks.Count; t++)
        {
            if(t > 0)
            {
                Instantiate(prefabSpace, transform);
            }

            var mtTrack = mtTrackSequencer.mtTrackScrObj.mtTracks[t];
            if (mtTrack.isSilent)
            {
                InstantiateSilent(mtTrack.silenceDuration, trackIndex);
                trackIndex++;
            }
            else
            {
                InstantiateTrackNumber(trackNumber);
                trackNumber++;
                for(int s = 0; s < mtTrack.trackSections.Count; s ++)
                {
                    InstantiateTrackSection(mtTrack.trackSections[s], s, trackIndex);
                    trackIndex++;
                }
            }
        }

        foreach (Image image in currentPlayImages)
        {
            image.color = ColorModifiers.ColorAlpha(image.color, 0f);
        }
        currentPlayImages[0].color = ColorModifiers.ColorAlpha(currentPlayImages[0].color, 1f);
    }

    private void ClearTracks()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    private void SetPlayCurrent(int index)
    {
        foreach (Image image in currentPlayImages)
        {
            image.color = ColorModifiers.ColorAlpha(image.color, 0f);
        }
        currentPlayImages[index].color = ColorModifiers.ColorAlpha(currentPlayImages[index].color, 1f);

        float scrollValue = index * (1f / (currentPlayImages.Count - 1));
        scrollBar.value = 1f - scrollValue;

    }

    private void InstantiateSilent(float duration, int trackIndex)
    {
        GameObject prefab = Instantiate(prefabTrackSilence,transform);
        SetToggleGroup(prefab, trackIndex);
        //currentPlayObjects.Add(prefab.transform.Find("Track_Silence_Frame/PlayCurrent").gameObject);
        currentPlayImages.Add(prefab.transform.Find("Track_Silence_Frame/PlayCurrent").gameObject.GetComponent<Image>());

        TextMeshProUGUI textSilence = prefab.transform.Find("Track_Silence_Frame/Track_Number_Text").GetComponent<TextMeshProUGUI>();
        string silence = "Silence " + duration.ToString("00") + "\"";
        textSilence.text = silence;

        LayoutElement layoutElement = prefab.GetComponent<LayoutElement>();
        layoutElement.preferredHeight = 40 + (5 * duration);
    }

    private void InstantiateTrackSection(MusicalTextureTrackSection mtTrackSection, int sectionNumber, int trackIndex)
    {
        GameObject prefab = Instantiate(prefabTrackSection, transform);
        SetToggleGroup(prefab, trackIndex);
        //currentPlayObjects.Add(prefab.transform.Find("PlayCurrent").gameObject);
        currentPlayImages.Add(prefab.transform.Find("PlayCurrent").gameObject.GetComponent<Image>());

        TextMeshProUGUI textSectionNumber = prefab.transform.Find("SectionNumber_Text").GetComponent<TextMeshProUGUI>();
        textSectionNumber.text = (sectionNumber + 1) + ".";

        SetMusicalTexture(prefab.transform.Find("MusicalTexture_Main"), mtTrackSequencer.mtTrackScrObj.musicalTexturesData.musicalTextures[mtTrackSection.mtMainIndex]);

        switch (mtTrackSection.trackSectionType)
        {
            case (TrackSectionType.Single):
                prefab.transform.Find("MusicalTexture_Secondary").gameObject.SetActive(false);
                break;
            case (TrackSectionType.Dual):
                SetMusicalTexture(prefab.transform.Find("MusicalTexture_Secondary"), mtTrackSequencer.mtTrackScrObj.musicalTexturesData.musicalTextures[mtTrackSection.mtSecondaryIndex]);
                break;
        }
    }

    private void SetMusicalTexture(Transform parent, MusicalTexture musicalTexture)
    {
        //Debug.Log("InstantiateMusicalTexture(" + parent.name + ", " + musicalTexture.textureName + ")");

        TextMeshProUGUI textMusicalTexture = parent.Find("Layout/Section_Element_Text").GetComponent<TextMeshProUGUI>();
        textMusicalTexture.text = musicalTexture.textureName;

        Image image = parent.Find("Layout/Image").GetComponent<Image>();
        image.color = musicalTexture.color;
    }

    private void InstantiateTrackNumber(int number)
    {
        GameObject prefab = Instantiate(prefabTrackNumber, transform);

        TextMeshProUGUI textTrackNumber = prefab.transform.Find("Track_Number_Text").GetComponent<TextMeshProUGUI>();
        textTrackNumber.text = "Track " + number.ToString("00");
    }

    private void SetToggleGroup(GameObject go, int trackIndex)
    {
        Toggle toggle = go.GetComponent<Toggle>();
        if(toggle != null)
        {
            toggle.group = toggleGroup;
            //toggle.onValueChanged.AddListener(OnValueChanged);
            toggle.onValueChanged.AddListener(delegate { mtTrackSequencer.SetSequenceIndex(trackIndex);  });
            toggle.onValueChanged.AddListener(SetToggleGroupAllowSwithOff);
        }
    }

    private void SetToggleGroupAllowSwithOff(bool value)
    {
        if(value && toggleGroup.allowSwitchOff)
        {
            toggleGroup.allowSwitchOff = false;
        }
    }
}
