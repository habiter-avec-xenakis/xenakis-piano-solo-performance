using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VertexSelectorZoneDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEventBool onPointerEnterExit = new UnityEventBool();
    //public UnityEvent onPointerExit = new UnityEvent();

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnterExit.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerEnterExit.Invoke(false);
    }
}