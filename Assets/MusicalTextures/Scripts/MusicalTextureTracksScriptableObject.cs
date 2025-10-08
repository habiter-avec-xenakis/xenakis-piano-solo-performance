using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicalTexturesTracks", menuName = "Musical Textures Tracks", order = 1)]
public class MusicalTextureTracksScriptableObject : ScriptableObject
{
    public MusicalTextureScriptableObject musicalTexturesData;
    public List<MusicalTextureTrack> mtTracks = new List<MusicalTextureTrack>();
}
