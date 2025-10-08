using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Klak.Timeline.Midi;
using System.IO;


public enum InfoType { Path, TimeStart, TimeEnd }

public class DataGetSequence : MonoBehaviour
{
    // Paths
    public string pathData;
    private string pathSequences;
    private string[] sequences;

    //public TimelineAsset timeline;
    public PlayableDirector director;
    public AudioSource audioSource;
    private TimelineAsset timeline;

    private string dataType = "";

    private void Start()
    {
        pathSequences = pathData + "/sequences";
        SequenceGet();

        if(sequences != null && sequences.Length != 0)
        {
            SequenceSet(sequences[0]);
        }
    }

    private void SequenceGet()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(pathSequences);
        FileInfo[] fileInfo = dirInfo.GetFiles();

        List<string> sequenceList = new List<string>();

        foreach(FileInfo file in fileInfo)
        {
            if(Path.GetExtension(file.ToString()) == ".txt")
            {
                sequenceList.Add(file.ToString());
            }
        }

        sequences = sequenceList.ToArray();
    }

    private void SequenceSet(string path)
    {
        timeline = new TimelineAsset();

        AudioTrack audioTrack = timeline.CreateTrack<AudioTrack>();
        VideoScriptPlayableTrack videoTrack = timeline.CreateTrack<VideoScriptPlayableTrack>();

    // Parse text file
    StreamReader sr = new StreamReader(path);
        while(!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            switch(line)
            {
                case ("[VIDEO]"):
                    dataType = "[VIDEO]";
                    break;
                case ("[AUDIO]"):
                    dataType = "[AUDIO]";
                    break;
                case ("[MIDI]"):
                    dataType = "[MIDI]";
                    break;
                default:
                    switch(dataType)
                    {
                        case ("[VIDEO]"):
                            TimelineClip videoClip = videoTrack.CreateClip<VideoScriptPlayableAsset>();
                            videoClip.start = FormattedTimeToSeconds(InfoGet(line, InfoType.TimeStart));
                            videoClip.duration = FormattedTimeToSeconds(InfoGet(line, InfoType.TimeEnd)) - videoClip.start;
                            break; 
                    }
                    break;
            }
        }

        // Video


        director.playableAsset = timeline;
    }

    private string InfoGet(string infoLine, InfoType infoType)
    {
        string info = "";
        string[] infoSplit = infoLine.Split(',');

        switch(infoType)
        {
            case (InfoType.Path):
                info = infoSplit[0];
                break;
            case (InfoType.TimeStart):
                info = infoSplit[1];
                break;
            case (InfoType.TimeEnd):
                info = infoSplit[2];
                break;
        }

        return info;
    }

    int FormattedTimeToSeconds(string formattedTime)
    {
        string[] timeSplit = formattedTime.Split(':');
        return (int.Parse(timeSplit[0]) * 60 + int.Parse(timeSplit[1]));
    }
}
