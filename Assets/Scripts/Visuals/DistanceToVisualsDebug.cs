using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceToVisualsDebug : MonoBehaviour
{
    public DistanceToVisuals distanceToVisuals;
    private float debugScale;

    public RectTransform gaugeParent;
    public RectTransform currentValue;
    public LayoutElement min;
    public LayoutElement currentZone;
    //public LayoutElement max;

    public TextMeshProUGUI currentZero;
    public TextMeshProUGUI currentOne;
    public TextMeshProUGUI valueMax;

    public Camera cam;
    public RectTransform gizmosParent;
    public RectTransform transformAGizmo;
    public RectTransform transformBGizmo;

    private Canvas canvas;
    private RectTransform canvasRecttransform;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRecttransform = canvas.GetComponent<RectTransform>();
    }

    private void Update()
    {
        debugScale = (gaugeParent.rect.yMax - gaugeParent.rect.yMin) / distanceToVisuals.valueMax;

        min.preferredHeight = distanceToVisuals.valueMin * debugScale;
        currentZone.preferredHeight = (distanceToVisuals.valueMaxSmoothed - distanceToVisuals.valueMin) * debugScale;
        //max.preferredHeight = (distanceToVisuals.valueMax - distanceToVisuals.valueMaxSmoothed) * debugScale;
        currentValue.anchoredPosition = new Vector2(0, distanceToVisuals.valueRaw * debugScale);

        currentZero.rectTransform.anchoredPosition = new Vector2(-10, distanceToVisuals.valueMin * debugScale);
        currentOne.rectTransform.anchoredPosition = new Vector2(-10, distanceToVisuals.valueMaxSmoothed * debugScale);
        valueMax.text = distanceToVisuals.valueMax.ToString("0.000");


        var screenPointA = RectTransformUtility.WorldToScreenPoint(cam, distanceToVisuals.transformA.position);
        transformAGizmo.anchoredPosition = screenPointA - canvasRecttransform.sizeDelta / 2f;

        var screenPointB = RectTransformUtility.WorldToScreenPoint(cam, distanceToVisuals.transformB.position);
        transformBGizmo.anchoredPosition = screenPointB - canvasRecttransform.sizeDelta / 2f;

        if(Input.GetKeyDown(KeyCode.D))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}