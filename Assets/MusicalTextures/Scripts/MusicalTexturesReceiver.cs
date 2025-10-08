using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicalTexturesReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private MusicalTextureSequencer musicalTextureSequencer;
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is MusicalTexturesSequencerMarker mtSeqMarker && musicalTextureSequencer != null)
        {
            Debug.Log(notification);
            musicalTextureSequencer.SetSequenceIndex(mtSeqMarker.Index);
        }
    }
}