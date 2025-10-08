using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Klak.Timeline.Midi;

public class DataUIManager : MonoBehaviour
{
    public DataPlayback dataPlayback;
    public DataGet dataGet;

    public RawImage video1;
    public RawImage video2;

    // UI Elements
    [Header("UI Elements")]
    public TMP_Dropdown dropdownDatasets;
    public Transform midiNotesParent;
    private Image[] midiNotes;

    // Audio
    [Header("Audio")]

    public GameObject audioSpectrumBar;
    public Transform audioSpectrumParent;

    public int audioSpectrumSampleCount = 64;
    public RectTransform spectrumPanel;
    public Vector2 spectrumSize;

    public Transform[] audioSpectrumBars;

    void Awake()
    {
        SpectrumInit();
        GetMidiNotes(midiNotesParent);
        dataGet.dataSetsFound.AddListener(DropdownDatasetListInit);
        dropdownDatasets.onValueChanged.AddListener(delegate { SetDataSet(dropdownDatasets); });
        dataPlayback.isPlaying.AddListener(OnPlay);
        dataPlayback.isStopping.AddListener(OnStop);
        OnStop();
    }

    void SpectrumInit()
    {
        audioSpectrumBars = new Transform[audioSpectrumSampleCount];
        for (int i = 0; i < audioSpectrumSampleCount / 2; i++)
        {
            Transform bar = Instantiate(audioSpectrumBar, Vector3.zero, Quaternion.identity).transform;
            bar.SetParent(audioSpectrumParent);
            bar.name = "bar_" + i.ToString("00");
            audioSpectrumBars[i] = bar;
        }
    }

    void DropdownDatasetListInit()
    {
        Debug.Log("DropdownDatasetListInit() " + dataGet.dataSets.Length);

        dropdownDatasets.ClearOptions();
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();

        foreach (DataSet dataSet in dataGet.dataSets)
        {
            string name = Path.GetFileNameWithoutExtension(dataSet.pathAudio);
            optionDatas.Add(new TMP_Dropdown.OptionData(name.Split('_')[0])); 
        }

        dropdownDatasets.AddOptions(optionDatas);
        dataPlayback.SetDataSet(dataGet.dataSets[dropdownDatasets.value]);
    }

    void SetDataSet(TMP_Dropdown dropdown)
    {
        dataPlayback.SetDataSet(dataGet.dataSets[dropdown.value]);
    }

    void Update()
    {
          UpdateSpectrum();
    }

    void UpdateSpectrum()
    {
        float[] spectrum = new float[audioSpectrumSampleCount];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float step = spectrumSize.x / audioSpectrumSampleCount;

        for (int i = 0; i < spectrum.Length / 2; i++)
        {
            audioSpectrumBars[i].localScale = new Vector3(1, Mathf.Clamp(spectrum[i], 0.005f, 1f), 1);
        }
    }

    private void OnPlay()
    {
        video1.enabled = true;
        video2.enabled = true;
    }

    private void OnStop()
    {
        video1.enabled = false;
        video2.enabled = false;

        foreach (Image note in midiNotes)
        {
            note.fillAmount = 1f;
        }
    }

    private void GetMidiNotes(Transform parent)
    {
        List<Image> midiNotesList = new List<Image>();
        foreach(Transform t in parent)
        {
            if(t.name.Contains("MidiNote"))
            {
                midiNotesList.Add(t.Find("Bar").GetComponent<Image>());
            }
        }
        midiNotes = midiNotesList.ToArray();
    }
}
