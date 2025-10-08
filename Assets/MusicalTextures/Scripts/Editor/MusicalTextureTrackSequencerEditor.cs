//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(MusicalTextureTrackSequencer))]
//public class MusicalTextureTrackSequencerEditor : Editor
//{
//    MusicalTextureTrackSequencer mtTrackSequencer;
//    List<MusicalTextureTrack> mtTracks = null;

//    private int[] mtIndices;
//    private string[] mtNames;

//    public void OnEnable()
//    {
//        mtTrackSequencer = (MusicalTextureTrackSequencer)target;

//        if(mtTrackSequencer.mtTrackScrObj != null)
//        {
//            mtTracks = mtTrackSequencer.mtTrackScrObj.mtTracks;
//        }

//        // ! DIRTY !
//        if (mtTrackSequencer.mtManager == null)
//        {
//            mtTrackSequencer.mtManager = mtTrackSequencer.GetComponent<MusicalTextureManager>();
//        }
//    }

//    public override void OnInspectorGUI()
//    {
//        mtTrackSequencer.mtManager = (MusicalTextureManager)EditorGUILayout.ObjectField("Musical Texture Manager", mtTrackSequencer.mtManager, typeof(MusicalTextureManager), true);
//        mtTrackSequencer.mtTrackScrObj = (MusicalTextureTracksScriptableObject)EditorGUILayout.ObjectField("Musical Textures Tracks", mtTrackSequencer.mtTrackScrObj, typeof(MusicalTextureTracksScriptableObject), false);

//        if (mtTracks == null)
//        {
//            EditorGUILayout.LabelField("Please set Tracks.");
//            return;
//        }

//        for(int i = 0; i < mtTracks.Count; i++)
//        {
//            EditorGUILayout.BeginHorizontal();

//            EditorGUILayout.LabelField(i.ToString("00"));

//            EditorGUILayout.EndHorizontal();
//        }
//    }
//}