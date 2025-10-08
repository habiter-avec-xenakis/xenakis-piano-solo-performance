using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MusicalTextureTrackReceiver : MonoBehaviour, INotificationReceiver
{
    [SerializeField] private MusicalTextureTrackSequencer mtTrackSequencer;
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is MusicalTextureTrackMarker mtTrackMarker && mtTrackSequencer != null)
        {
            //Debug.Log(notification);
            mtTrackSequencer.NextSection();
            //mtTrackSequencer.SetSequenceIndex(mtTrackMarker.Index);
        }
    }
}