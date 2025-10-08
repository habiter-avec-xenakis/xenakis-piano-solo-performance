using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CustomStyle("TrackSequencerMarker")]
public class MusicalTextureTrackMarker : Marker, INotification, INotificationOptionProvider
{
    [SerializeField] private int index = 0;

    [Space(20)]
    [SerializeField] private bool retroactive = true;
    [SerializeField] private bool emitOnce = true;

    public PropertyName id => new PropertyName();
    public int Index => index;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
