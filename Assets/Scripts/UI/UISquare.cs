using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class UISquare : MonoBehaviour
{
    private LayoutElement layoutElement;
    private RectTransform rectTransform;

    private void Start()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(rectTransform.rect.height);
    }

    private void Update()
    {
        layoutElement.preferredWidth = rectTransform.rect.height;
    }
}
