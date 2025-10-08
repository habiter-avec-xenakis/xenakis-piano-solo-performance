using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HermaControls : MonoBehaviour
{
    public VisualEffect vfxLasers;

    public Color expoAColorLeft = Color.red;
    public Color expoAColorRight = Color.blue;

    public Color expoBColorLeft = Color.green;
    public Color expoBColorRight = Color.magenta;

    public Color expoCColorLeft = Color.yellow;
    public Color expoCColorRight = Color.cyan;

    private bool allColors = false;
    private Color[] colors;
    private float delay = 0.25f;
    private float timer = 0f;

    private void Start()
    {
        colors = new Color[] { expoAColorLeft, expoAColorRight, expoBColorLeft, expoBColorRight, expoCColorLeft, expoCColorRight};
    }

    private void Update()
    {
        if(allColors)
        {
            timer += Time.deltaTime;
            if(timer > delay)
            {
                vfxLasers.SetVector4("Color Left", colors[Random.Range(0,colors.Length - 1)]);
                vfxLasers.SetVector4("Color Right", colors[Random.Range(0, colors.Length - 1)]);
                timer = 0f;
            }
        }
    }

    private void SetLaserColors(Color colorLeft, Color colorRight)
    {
        vfxLasers.SetVector4("Color Left", colorLeft);
        vfxLasers.SetVector4("Color Right", colorRight);
    }

    public void SetExpositionR()
    {
        allColors = false;
        SetLaserColors(Color.white, Color.white);
    }

    public void SetExpositionA()
    {
        allColors = false;
        SetLaserColors(expoAColorLeft, expoAColorRight);
    }

    public void SetExpositionB()
    {
        allColors = false;
        SetLaserColors(expoBColorLeft, expoBColorRight);
    }

    public void SetExpositionC()
    {
        allColors = false;
        SetLaserColors(expoCColorLeft, expoCColorRight);
    }

    public void SetDevelopmentABC()
    {
        allColors = true;
    }

    public void SetResultFClimax()
    {
        allColors = false;
    }
}
