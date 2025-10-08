using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIMainSequenceManager : MonoBehaviour
{
    public MainSequenceManager mainSequenceManager;

    public CanvasGroup canvasGroup;

    [Header("Panels")]
    public TextMeshProUGUI topBarTitle;
    public GameObject panelMain;
    //public GameObject panelOptions;
    private bool optionsOn = false;
    public GameObject subSequencePanel;
    public TextMeshProUGUI subSequenceText;

    [Header("Toggles")]
    public ToggleGroup toggleGroup;
    public GameObject togglePrefab;
    Toggle[] togglesInstanced;

    public int instantiationOffset = 0;

    [Header("Buttons")]
    public Button buttonPrevious;
    public Button buttonNext;
    //public Image buttonOptionsImage;
    //public Sprite optionsBackSprite;
    //private Sprite optionsSpriteOriginal;

    private int index = 0;

    private EventSystem eventSystem;

    private void Awake()
    {
        PerformanceGlobalManager.subSequencePanel = subSequencePanel;

        // Getting private variables
        //optionsSpriteOriginal = buttonOptionsImage.sprite;
        eventSystem = FindObjectOfType<EventSystem>();

        // Destroy existing toggles
        foreach (Transform t in toggleGroup.transform)
        {
            if (t.name.Contains("Toggle"))
            {
                Destroy(t.gameObject);
            }
        }

        // Instantiate new toggles
        togglesInstanced = new Toggle[mainSequenceManager.mainSequenceItems.Length];

        for(int i = 0; i < mainSequenceManager.mainSequenceItems.Length; i++)
        {
            var seqItem = mainSequenceManager.mainSequenceItems[i];

            var toggle = Instantiate(togglePrefab, toggleGroup.transform);
            toggle.transform.SetSiblingIndex(i + instantiationOffset);
            toggle.name = toggle.name.Replace("(Clone)", "_" + seqItem.name.Replace(" ", "_").Replace(".",""));

            var textComponent = toggle.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = seqItem.name;

            var toggleComponent = toggle.GetComponentInChildren<Toggle>();
            toggleComponent.group = toggleGroup;
            togglesInstanced[i] = toggleComponent;
        }

        for(int t = 0; t < togglesInstanced.Length; t++)
        {
            var index = t;
            if(t == 0)
            {
                togglesInstanced[t].isOn = true;
            }

            togglesInstanced[t].onValueChanged.AddListener(delegate { mainSequenceManager.ItemSet(index); });
        }

        // Adding listeners
        mainSequenceManager.onReachedFirst.AddListener(OnReachedFirst);
        mainSequenceManager.onReachedLast.AddListener(OnReachedLast);
        mainSequenceManager.onLeftFirst.AddListener(OnLeftFirst);
        mainSequenceManager.onLeftLast.AddListener(OnLeftLast);
        mainSequenceManager.onItemChanged.AddListener(SetToggle);
        mainSequenceManager.onSceneLoaded.AddListener(OnSceneLoaded);

        //SetOptions(optionsOn);
    }

    //public void SetOptions()
    //{
    //    SetOptions(!optionsOn);
    //    eventSystem.SetSelectedGameObject(null);
    //}

    //private void SetOptions(bool activate)
    //{
    //    panelOptions.SetActive(activate);
    //    panelMain.SetActive(!activate);

    //    if(activate)
    //    {
    //        topBarTitle.text = "Options";
    //        buttonOptionsImage.sprite = optionsBackSprite;
    //    }
    //    else
    //    {
    //        topBarTitle.text = "Controls";
    //        buttonOptionsImage.sprite = optionsSpriteOriginal;
    //    }

    //    optionsOn = activate;
    //}

    private void OnSceneLoaded()
    {
        canvasGroup.blocksRaycasts = true;
        eventSystem.SetSelectedGameObject(null);
    }

    private void SetToggle(int index)
    {
        canvasGroup.blocksRaycasts = false;
        //Debug.Log("SetToggle(" + index + ")");
        togglesInstanced[index].SetIsOnWithoutNotify(true);
        subSequenceText.text = mainSequenceManager.mainSequenceItems[index].name;
    }

    private void OnReachedFirst()
    {
        //Debug.Log("OnReachedFirst()");
        buttonPrevious.interactable = false;
    }

    private void OnReachedLast()
    {
        //Debug.Log("OnReachedLast()");
        buttonNext.interactable = false;
    }

    private void OnLeftFirst()
    {
        //Debug.Log("OnLeftFirst()");
        buttonPrevious.interactable = true;
    }

    private void OnLeftLast()
    {
        //Debug.Log("OnLeftLast()");
        buttonNext.interactable = true;
    }
}