using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ColorTools;

public class EvryaliIntroSequence : MonoBehaviour
{
    //private MusicalTextureTrackSequencer mtTrackSequencer;
    //public bool skipIntro = false;

    ////[Header("Title")]
    ////public TextMesh title;
    ////public float titleFadeTime = 5f;

    ////[Header("Avatar")]
    ////public Slider sliderAvatarFade;
    ////public float avatarFadeTime = 5f;

    //[Header("Intro")]
    //public GameObject introPanel;

    //public Button buttonTitle;
    //public Button buttonAvatar;
    //public Button buttonStart;

    //[Header("UI")]
    //public GameObject trackSelectionPanel;
    //public GameObject firstPanel;
    //public GameObject secondPanel;
    ////public GameObject thirdPanel;

    //public UnityEvent m_onIntroEnd = new UnityEvent();

    //private void Awake()
    //{
    //    IntroParameters.skipIntro = skipIntro;
    //    mtTrackSequencer = FindObjectOfType<MusicalTextureTrackSequencer>();
    //}

    //private void Start()
    //{
    //    if (IntroParameters.skipIntro)
    //    {
    //        title.color = new Color(1f, 1f, 1f, 0f);
    //        //sliderAvatarFade.value = 1f;
    //        introPanel.SetActive(false);
    //        m_onIntroEnd.Invoke();
    //        Begin();
    //        return;
    //    }

    //    trackSelectionPanel.SetActive(false);
    //    firstPanel.SetActive(false);
    //    secondPanel.SetActive(false);
    //    //thirdPanel.SetActive(false);

    //    //sliderAvatarFade.interactable = false;
    //    buttonAvatar.interactable = false;
    //    buttonStart.interactable = false;
    //}

    //public void BeginTitleFadeOut()
    //{
    //    StartCoroutine(TextMeshFadeOut(title, titleFadeTime));

    //    buttonTitle.interactable = false;
    //    buttonAvatar.interactable = true;
    //}

    //public void BeginAvatarFadeIn()
    //{
    //    //StartCoroutine(SliderFadeIn(sliderAvatarFade, avatarFadeTime));

    //    buttonAvatar.interactable = false;
    //    buttonStart.interactable = true;
    //}

    //IEnumerator TextMeshFadeOut(TextMesh textMesh, float duration)
    //{
    //    float elapsedTime = 0;

    //    while (elapsedTime < duration)
    //    {
    //        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
    //        textMesh.color = ColorModifiers.ColorAlpha(textMesh.color, alpha);
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    textMesh.color = new Color(1f, 1f, 1f, 0f);
    //}

    //IEnumerator SliderFadeIn(Slider slider, float duration)
    //{
    //    float elapsedTime = 0;

    //    while (elapsedTime < duration)
    //    {
    //        slider.value = elapsedTime / duration;
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    slider.value = 1f;
    //    slider.interactable = true;
    //}

    //public void Begin()
    //{
    //    introPanel.SetActive(false);

    //    trackSelectionPanel.SetActive(true);
    //    firstPanel.SetActive(true);
    //    secondPanel.SetActive(true);
    //    //thirdPanel.SetActive(true);

    //    mtTrackSequencer.SetSequenceIndex(0);

    //    m_onIntroEnd.Invoke();
    //}

}
