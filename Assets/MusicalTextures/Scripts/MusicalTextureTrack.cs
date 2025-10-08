using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackSectionType { Single, Dual }

[System.Serializable]
public class MusicalTextureTrack
{
    public bool isSilent = false;
    public float silenceDuration = 1f;

    public string comment;
    public Texture2D sketch;
    public List <MusicalTextureTrackSection> trackSections;
}

[System.Serializable]
public class MusicalTextureTrackSection
{
    public TrackSectionType trackSectionType;

    public int mtMainIndex;
    public int mtSecondaryIndex;

    //public MusicalTexture mtMain;
    //public MusicalTexture mtSecondary;
}