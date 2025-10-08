using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICuesManager : MonoBehaviour
{
    public MainSequenceManager mainSequenceManager;
    public CanvasGroup canvasGroup;
    public RectTransform cuesTransform;
    private float cuesWidth;
    private float currentWidth;

    public GameObject buttonsPanel;

    public RectTransform content;
    public LayoutElement prefillLayoutElement;
    public LayoutElement postfillLayoutElement;
    public GameObject cursor;
    private RectTransform currentPrefab;

    private float time;
    private float referenceTime;
    public UITimer uiTimer;
    public GameObject buttonStart;
    private bool progression = false;

    private void Start()
    {
        buttonsPanel.SetActive(false);
        mainSequenceManager.onItemChanged.AddListener(OnItemChanged);
        cursor.SetActive(false);
        canvasGroup.alpha = 0f;
        StartCoroutine(InitRectTransformsValues());
    }

    private void Update()
    {
        if(progression)
        {
            time += Time.deltaTime;
            var progressionValue = Mathf.Clamp01(time / referenceTime);
            if(time > referenceTime)
            {
                progression = false;
                uiTimer.TimerStop();
            }
            SetValue(progressionValue);
        }
    }

    IEnumerator InitRectTransformsValues()
    {
        yield return new WaitForEndOfFrame();
        cuesWidth = cuesTransform.sizeDelta.x;
        prefillLayoutElement.preferredWidth = cuesWidth / 2f;
        postfillLayoutElement.preferredWidth = cuesWidth / 2f;
        cuesTransform.gameObject.SetActive(false);
    }

    IEnumerator SetCues(GameObject prefab)
    {
        if (currentPrefab)
        {
            Destroy(currentPrefab.gameObject);
        }

        var prefabInstance = Instantiate(prefab);
        currentPrefab = prefabInstance.GetComponent<RectTransform>();
        currentPrefab.transform.SetParent(content,false);
        currentPrefab.transform.SetSiblingIndex(1);

        yield return new WaitForEndOfFrame();

        currentWidth = currentPrefab.sizeDelta.x;
    }

    private void OnItemChanged(int index)
    {
        var mainSequenceItem = mainSequenceManager.mainSequenceItems[index];
        referenceTime = mainSequenceItem.referenceTime;
        var cuesPrefab = mainSequenceItem.cuesPrefab;

        time = 0f;
        progression = false;
        uiTimer.TimerReset();
        canvasGroup.alpha = 0f;
        cursor.SetActive(false);

        if (cuesPrefab)
        {
            cuesTransform.gameObject.SetActive(true);
            buttonStart.SetActive(true);
            uiTimer.TimerStop();
            StartCoroutine(SetCues(cuesPrefab));
        }
        else
        {
            cuesTransform.gameObject.SetActive(false);
            uiTimer.TimerStart();
        }
    }

    private void SetValue(float value)
    {
        if (currentPrefab)
        {
            content.anchoredPosition = new Vector2(-value * currentWidth, content.anchoredPosition.y);
        }
    }

    public void BeginProgression()
    {
        progression = true;
        buttonStart.SetActive(false);
        cursor.SetActive(true);
        uiTimer.TimerStart();
        canvasGroup.alpha = 1f;
        buttonsPanel.SetActive(true);
    }

    public void PlayPause()
    {
        progression = !progression;
        if(progression == true)
        {
            uiTimer.TimerStart();
        }
        else
        {
            uiTimer.TimerStop();
        }
    }

    public void ResetTime()
    {
        progression = false;
        SetValue(0f);
        time = 0f;
        uiTimer.TimerReset();
        uiTimer.TimerStop();
    }
}