using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UILeftRightFromHandsDebug : MonoBehaviour
{
    public LeftRightFromHands lrfh;
    public LeftRightFromHandsDebug lrfhd;

    [Header("UI Objects")]
    public TextMeshProUGUI debugButtonText;
    public GameObject debugCursorPrefab;
    public Slider slider;
    public RectTransform handleSlideArea;
    private float handleSlideAreaWidth = 1f;
    private RectTransform handle;
    public GameObject[] panels;

    [Header("UI Text")]
    public Color textDebugWarningColor = Color.red;

    public TextMeshProUGUI textRawLeftMax;
    public TextMeshProUGUI textRawRightMax;
    public TextMeshProUGUI textRawLeft;
    public TextMeshProUGUI textRawRight;
    public TextMeshProUGUI textRawAverageLeft;
    public TextMeshProUGUI textRawAverageRight;
    public TextMeshProUGUI textRawAverage;

    public TextMeshProUGUI textNormalizedLeftMax;
    public TextMeshProUGUI textNormalizedRightMax;
    public TextMeshProUGUI textNormalizedAverage;

    [Header("Cursors")]
    public Color cursorLeftColor = Color.red;
    private RectTransform cursorLeft;

    public Color cursorRightColor = Color.blue;
    private RectTransform cursorRight;

    public Color cursorAveragesmoothedColor = Color.green;
    private RectTransform cursorAveragesmoothed;

    [Header("Options")]
    public bool createCamera = true;
    private Camera cam;

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        StartCoroutine(GetHandleSlideAreaWidth());

        cursorLeft = Cursor("Cursor_Left", cursorLeftColor);
        cursorRight = Cursor("Cursor_Right", cursorRightColor);
        cursorAveragesmoothed = Cursor("Cursor_AverageSmoothed", cursorAveragesmoothedColor);

        if(createCamera)
        {
            GameObject camObject = new GameObject();

            camObject.transform.SetParent(lrfh.boneReference.parent, false);
            camObject.name = "Camera_Debug";

            cam = camObject.AddComponent<Camera>();
            cam.cullingMask = (1 << 10);
            cam.fieldOfView = 5;
            cam.rect = new Rect(0.65f, 0.9f, 0.35f, 0.1f);
            cam.clearFlags = CameraClearFlags.Color;
            cam.backgroundColor = new Color(0, 0, 0, 0);
        }
    }

    private void Update()
    {
        if(!lrfh)
        {
            return;
        }

        slider.value = lrfh.normalizedAverageRaw;

        cursorLeft.anchoredPosition = new Vector2(-lrfh.normalizedLeft * handleSlideAreaWidth / 2f, 0);
        cursorRight.anchoredPosition = new Vector2(lrfh.normalizedRight * handleSlideAreaWidth / 2f, 0);
        cursorAveragesmoothed.anchoredPosition = new Vector2(lrfh.normalizedAverageSmoothed * handleSlideAreaWidth, 0);

        // Text debug
        textRawLeftMax.text = FormattedValue(lrfh.maxLeft);
        textRawRightMax.text = FormattedValue(lrfh.maxRight);
        textRawLeft.text = FormattedValue(lrfh.rawLeft);
        textRawRight.text = FormattedValue(lrfh.rawRight);

        textRawAverageLeft.text = FormattedValue(lrfh.averageMaxLeft);
        textRawAverageRight.text = FormattedValue(lrfh.averageMaxRight);

        textRawAverage.text = FormattedValue(lrfh.rawAverage);

        SetNormalizedValue(textNormalizedLeftMax, lrfh.normalizedLeft);
        SetNormalizedValue(textNormalizedRightMax, lrfh.normalizedRight);
        SetNormalizedValue(textNormalizedAverage, lrfh.normalizedAverageRaw);

        if (cam)
        {
            cam.transform.localPosition = new Vector3(0, 0, -3.25f);
        }
    }

    public void Switch()
    {
        foreach(var p in panels)
        {
            p.SetActive(!p.activeSelf);
        }
        cam.enabled = !cam.enabled;
        eventSystem.SetSelectedGameObject(null);

        if(cam.enabled)
        {
            debugButtonText.text = "Debug ON";
        }
        else
        {
            debugButtonText.text = "Debug OFF";
        }
    }

    RectTransform Cursor(string name, Color color)
    {
        GameObject cursorObject = Instantiate(debugCursorPrefab, slider.handleRect.parent);
        cursorObject.name = name;
        Image cursorImage = cursorObject.GetComponent<Image>();
        cursorImage.color = color;
        RectTransform cursor = cursorObject.GetComponent<RectTransform>();

        return cursor;
    }

    IEnumerator GetHandleSlideAreaWidth()
    {
        yield return new WaitForEndOfFrame();
        handleSlideAreaWidth = handleSlideArea.rect.width;
    }

    string FormattedValue(float value)
    {
        return value.ToString("0.000");
    }

    private void SetNormalizedValue(TextMeshProUGUI text, float value)
    {
        text.text = FormattedValue(value);
        if(Mathf.Abs(value) > 1f)
        {
            text.color = textDebugWarningColor;
        }
        else
        {
            text.color = Color.white;
        }
    }
}