using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicalTextureSequencer : MonoBehaviour
{
    public MusicalTextureManager mtManager;

    public TextMeshProUGUI textCurrentSequence;
    public Renderer sketchesRenderer;
    private float sketchesRendererScaleBase;

    public int[] mtSeqIndexes;
    public string[] mtSeqComments;
    public Texture2D[] mtSeqSketches;

    private void Start()
    {
        sketchesRendererScaleBase = sketchesRenderer.transform.localScale.x;
        sketchesRenderer.enabled = false;
    }

    public void SetSequenceIndex(int index)
    {
        string textSequence = "";
        if (index == -1)
        {
            textSequence = mtManager.musicalTextureOff.textureName;
        }
        else
        {
            textSequence = (index + 1).ToString("00") + " | " + mtSeqComments[index] + " | " + mtManager.musicalTexturesData.musicalTextures[mtSeqIndexes[index]].textureName;
        }

        textCurrentSequence.text = textSequence;
        mtManager.SetMusicalTexture(mtSeqIndexes[index]);

        if (mtSeqSketches[index] != null)
        {
            sketchesRenderer.enabled = true;
            Texture2D sketch = mtSeqSketches[index];
            sketchesRenderer.material.SetTexture("mtSketch", sketch);
            float ratio = (float)sketch.width / (float)sketch.height;
            //Debug.Log("ratio " + ratio);

            float scale = sketchesRendererScaleBase;
            if (ratio > 1)
            {
                sketchesRendererScaleBase /= ratio / 2;
            }
            else if (ratio < 1)
            {
                sketchesRendererScaleBase /= ratio * 2;
            }

            sketchesRenderer.transform.localScale = new Vector3(scale * ratio, scale, 1);
        }
        else
        {
            sketchesRenderer.enabled = false;
        }
    }
}