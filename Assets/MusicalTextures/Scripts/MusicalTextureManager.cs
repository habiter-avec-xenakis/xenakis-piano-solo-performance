using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using TMPro;

public enum mtState { Idle, Transition }

public class MusicalTextureData
{
    public int indexCurrent = 0;
    public int indexNext = 0;
    public Gradient gradientCurrent;
    public MusicalTexture mtCurrent = new MusicalTexture();
    public MusicalTexture mtPrevious = new MusicalTexture();
}

public class MusicalTextureManager : MonoBehaviour
{
    private mtState mtState = mtState.Idle;
    private TrackSectionType m_trackSectionType;
    private TrackSectionType m_trackSectionTypeLast;
    public MusicalTexture musicalTextureOff;

    public float transitionSpeed = 1f;

    public MusicalTextureScriptableObject musicalTexturesData;
    private bool m_isOn = false;
    private float m_transitionValue = 0f;
    //private bool dualCooldown = false;

    private MusicalTextureData m_mtDataMain = new MusicalTextureData();
    private MusicalTextureData m_mtDataSecondary = new MusicalTextureData();

    public MusicalTextureData mtDataMain
    {
        get { return m_mtDataMain; }
    }

    public MusicalTextureData mtDataSecondary
    {
        get { return m_mtDataSecondary; }
    }

    public float transitionValue
    {
        get { return m_transitionValue; }
    }

    public bool isOn
    {
        get { return m_isOn; }
    }

    public TrackSectionType trackSectionType
    {
        get { return m_trackSectionType;  }
    }

    // Events
    public UnityEventColor onSingleColorUpdate = new UnityEventColor();
    public UnityEventGradient onSingleGradientUpdate = new UnityEventGradient();
    public UnityEventFloat onSingleRoughnessUpdate = new UnityEventFloat();
    public UnityEventFloat onSingleChaosUpdate = new UnityEventFloat();

    public UnityEventColorTwo onDualColorUpdate = new UnityEventColorTwo();
    public UnityEventGradientTwo onDualGradientUpdate = new UnityEventGradientTwo();
    public UnityEventFloatTwo onDualRoughnessUpdate = new UnityEventFloatTwo();
    public UnityEventFloatTwo onDualChaosUpdate = new UnityEventFloatTwo();


    void Start()
    {
        mtDataMain.mtCurrent = MusicalTextureCopy(musicalTextureOff);
        mtDataMain.mtPrevious = MusicalTextureCopy(musicalTextureOff);

        mtDataSecondary.mtCurrent = MusicalTextureCopy(musicalTextureOff);
        mtDataSecondary.mtPrevious = MusicalTextureCopy(musicalTextureOff);

        UpdateMusicalTexture(m_mtDataMain , 0,0f);
        UpdateMusicalTexture(m_mtDataSecondary, 0,0f);

        m_trackSectionTypeLast = m_trackSectionType;
    }

    void Update()
    {
        switch(mtState)
        {
            case (mtState.Idle):
                break;
            case (mtState.Transition):
                m_transitionValue += Time.deltaTime * transitionSpeed;

                if (m_transitionValue > 1f)
                {
                    m_transitionValue = 1f;
                    mtState = mtState.Idle;
                    UpdateMusicalTexture(m_mtDataMain, m_mtDataMain.indexNext, m_transitionValue);
                    m_mtDataMain.indexCurrent = m_mtDataMain.indexNext;


                    UpdateMusicalTexture(m_mtDataSecondary, m_mtDataSecondary.indexNext, m_transitionValue);
                    m_mtDataSecondary.indexCurrent = m_mtDataSecondary.indexNext;

                    mtState = mtState.Idle;
                }
                else
                {
                    UpdateMusicalTexture(m_mtDataMain, m_mtDataMain.indexNext, m_transitionValue);
                    UpdateMusicalTexture(m_mtDataSecondary, m_mtDataSecondary.indexNext, m_transitionValue);
                }


                switch(trackSectionType)
                {
                    case (TrackSectionType.Single):
                        onSingleColorUpdate.Invoke(m_mtDataMain.mtCurrent.color);
                        break;
                    case (TrackSectionType.Dual):
                        onDualColorUpdate.Invoke(m_mtDataMain.mtCurrent.color, m_mtDataSecondary.mtCurrent.color);
                        break;
                }

                break;
        }
    }

    public void SetMusicalTexture(int index)
    {
        m_trackSectionType = TrackSectionType.Single;
        m_transitionValue = 0f;

        switch (mtState)
        {
            case (mtState.Idle):
                m_mtDataMain.indexNext = index;
                mtState = mtState.Transition;
                if(!m_isOn)
                {
                    m_mtDataMain.mtPrevious = MusicalTextureCopy(musicalTextureOff);
                }
                else
                {
                    m_mtDataMain.mtPrevious = MusicalTextureCopy(musicalTexturesData.musicalTextures[m_mtDataMain.indexCurrent]);
                    m_mtDataSecondary.mtCurrent = MusicalTextureCopy(m_mtDataSecondary.mtCurrent);
                }
                break;
            case (mtState.Transition):

                m_mtDataMain.indexCurrent = m_mtDataMain.indexNext;
                m_mtDataMain.indexNext = index;
                m_mtDataMain.mtPrevious = MusicalTextureCopy(m_mtDataMain.mtCurrent);

                break;
        }

        m_isOn = true;
    }

    public void SetMusicalTextures(int indexMain, int indexSecondary)
    {
        m_trackSectionType = TrackSectionType.Dual;
        m_transitionValue = 0f;

        switch (mtState)
        {
            case (mtState.Idle):

                m_mtDataMain.indexNext = indexMain;
                m_mtDataSecondary.indexNext = indexSecondary;

                mtState = mtState.Transition;

                m_mtDataMain.mtPrevious = MusicalTextureCopy(musicalTexturesData.musicalTextures[m_mtDataMain.indexCurrent]);
                m_mtDataSecondary.mtPrevious = MusicalTextureCopy(musicalTexturesData.musicalTextures[m_mtDataSecondary.indexCurrent]);

                break;
            case (mtState.Transition):

                m_mtDataMain.indexCurrent = m_mtDataMain.indexNext;
                m_mtDataSecondary.indexCurrent = m_mtDataSecondary.indexNext;

                m_mtDataMain.indexNext = indexMain;
                m_mtDataSecondary.indexNext = indexSecondary;

                m_mtDataMain.mtPrevious = MusicalTextureCopy(m_mtDataMain.mtCurrent);
                m_mtDataSecondary.mtPrevious = MusicalTextureCopy(m_mtDataSecondary.mtCurrent);

                break;
        }

        m_isOn = true;
    }

    private void UpdateMusicalTexture(MusicalTextureData mtData, int index, float value)
    {
        MusicalTexture mtTarget;
        if (!m_isOn)
        {
            mtTarget = musicalTextureOff;
        }
        else
        {
            mtTarget = musicalTexturesData.musicalTextures[index];
        }

        Color lerpedColor = Color.Lerp(mtData.mtPrevious.color, mtTarget.color, value);
        Gradient lerpedGradient = GradientFromColor(lerpedColor);

        float lerpedRoughness = Mathf.Lerp(mtData.mtPrevious.roughness, mtTarget.roughness, value);
        float lerpedChaos = Mathf.Lerp(mtData.mtPrevious.chaos, mtTarget.chaos, value);
        float lerpedIntensity = Mathf.Lerp(mtData.mtPrevious.intensity, mtTarget.intensity, value);

        mtData.mtCurrent.color = lerpedColor;
        mtData.mtCurrent.roughness = lerpedRoughness;
        mtData.mtCurrent.chaos = lerpedChaos;
    }

    public void SetNextTexture()
    {
        int index = m_mtDataMain.indexCurrent + 1;
        if(mtState == mtState.Transition)
        {
            index = m_mtDataMain.indexNext + 1;
        }
        if(index > musicalTexturesData.musicalTextures.Count - 1)
        {
            index = 0;
        }
        SetMusicalTexture(index);
    }

    public void SetPreviousTexture()
    {
        int index = m_mtDataMain.indexCurrent - 1;
        if (mtState == mtState.Transition)
        {
            index = m_mtDataMain.indexNext - 1;
        }
        if (index < 0)
        {
            index = musicalTexturesData.musicalTextures.Count - 1;
        }
        SetMusicalTexture(index);
    }

    Vector4 ColorToV4(Color color)
    {
        return new Vector4(color.r, color.g, color.b, color.a);
    }

    Gradient GradientFromColor(Color color)
    {
        Gradient grad = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0].time = 0f;
        colorKeys[0].color = color;
        colorKeys[1].time = 0.5f;
        colorKeys[1].color = color;
        colorKeys[2].time = 1f;
        colorKeys[2].color = ShiftedColor(color, -0.2f, 0, 0);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[3];
        alphaKeys[0].time = 0f;
        alphaKeys[0].alpha = 1f;
        alphaKeys[1].time = 0.75f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[2].time = 1f;
        alphaKeys[2].alpha = 0f;

        grad.SetKeys(colorKeys, alphaKeys);

        return grad;
    }

    Color ShiftedColor(Color color, float hue, float saturation, float value)
    {
        float h;
        float s;
        float v;
        Color.RGBToHSV(color,out h,out s,out v);
        Color shiftedColor = Color.HSVToRGB(ShiftedFloat(h, hue), ShiftedFloat(s, saturation),ShiftedFloat(v, value));

        return shiftedColor;
    }

    float ShiftedFloat(float f, float value)
    {
        f += value;
        if (f < 0)
        {
            f += 1f;
        }
        if (f > 1f)
        {
            f -= 1f;
        }

        return f;
    }

    ParticleSystem.MinMaxCurve NoiseRemap(float roughness)
    {
        AnimationCurve curve = new AnimationCurve();

        Keyframe[] keyframes = new Keyframe[4];
        keyframes[0] = new Keyframe(0f, -1f, 0f, 0f, 0f, 0f);
        keyframes[1] = new Keyframe(0f + (roughness / 2f), -1f, 0f, 0f, 0f, 0f);
        keyframes[2] = new Keyframe(1f - (roughness / 2f), 1f, 0f, 0f, 0f, 0f);
        keyframes[3] = new Keyframe(1f, 1f, 0f, 0f, 0f, 0f);

        curve.keys = keyframes;

        ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(1f, curve);

        return minMaxCurve;
    }

    MusicalTexture MusicalTextureCopy(MusicalTexture mt)
    {
        MusicalTexture mtNew = new MusicalTexture();
        mtNew.textureName = mt.textureName;
        mtNew.color = mt.color;
        mtNew.roughness = mt.roughness;
        mtNew.chaos = mt.chaos;

        return mtNew;
    }

    string MusicalTextureDebug (MusicalTexture musicalTexture)
    {
        string debug = "";

        debug += "Roughness: " + musicalTexture.roughness + "\n";
        debug += "Chaos: " + musicalTexture.chaos;

        return debug;
    }

    public void MusicalTexturesEnable(bool enable)
    {
        m_isOn = enable;
    }
}
