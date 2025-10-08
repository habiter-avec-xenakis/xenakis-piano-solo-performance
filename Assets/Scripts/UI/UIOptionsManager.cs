using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIOptionsManager : MonoBehaviour
{
    public Canvas uiCanvas;
    public GameObject displayTogglePrefab;
    public Transform controlsDisplayParent;
    public ToggleGroup controlsDisplayToggleGroup;
    public Camera uiCamera;
    public Camera visualsCamera;
    private bool controlsDisplaysInit = false;
    public GameObject raycastBlock;
    public GameObject panelQuitConfirmation;

    private void Awake()
    {
        QuitPanelClose();

        foreach (Transform t in controlsDisplayParent)
        {
            if (t.name.Contains("Toggle"))
            {
                Destroy(t.gameObject);
            }
        }

#if UNITY_EDITOR
            for (int i = 0; i < 2; i++)
#else
            for (int i = 0; i < Display.displays.Length; i++)
#endif
            {
            int index = i;
            var displayToggle = (GameObject)Instantiate(displayTogglePrefab, controlsDisplayParent);
            displayToggle.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            var toggleComponent = displayToggle.GetComponent<Toggle>();
            toggleComponent.group = controlsDisplayToggleGroup;
            toggleComponent.onValueChanged.AddListener(delegate { SetControlsDisplay(index); });
        }

        //Debug.Log(uiCamera.targetDisplay);
    }

    public void SetControlsDisplay(int index)
    {
        if(controlsDisplaysInit)
        {
            uiCamera.targetDisplay = index;
            uiCanvas.targetDisplay = index;
            //PerformanceGlobalManager.onControlsDisplayChanged.Invoke(index);
        }
        else
        {
            controlsDisplaysInit = true;
        }
    }

    public void QuitPanelOpen()
    {
        raycastBlock.SetActive(true);
        panelQuitConfirmation.SetActive(true);
    }

    public void QuitPanelClose()
    {
        //Debug.Log("QuitPanelClose()");
        raycastBlock.SetActive(false);
        panelQuitConfirmation.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}