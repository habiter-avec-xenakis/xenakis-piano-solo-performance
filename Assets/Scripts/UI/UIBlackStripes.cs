using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlackStripes : MonoBehaviour
{
    public Canvas canvas;
    public Image blackStripe_Left;
    public Image blackStripe_Right;
    [Range(0f,0.5f)]
    public float blackStripes_Lateral;

    private void Start()
    {
        //Debug.Log(blackStripe_Left.rectTransform.sizeDelta);
        //Debug.Log(canvas.renderingDisplaySize);
        SetBlackStripesLateral();
    }

    //private void Update()
    //{
    //    SetBlackStripesLateral(value);
    //}

    public void SetBlackStripesLateral()
    {
        blackStripe_Left.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvas.renderingDisplaySize.x * blackStripes_Lateral);
        blackStripe_Right.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, canvas.renderingDisplaySize.x * blackStripes_Lateral);
    }
}
