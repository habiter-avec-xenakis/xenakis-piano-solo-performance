using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum MainSequenceItemType { Intro, Transition, Piece, Outro }

[System.Serializable]
public class MainSequenceItem
{
    public string name;
    public MainSequenceItemType mainSequenceItemType = MainSequenceItemType.Intro;
    public string sceneName;
    [HideInInspector]
    public int transitionIndex;
    public GameObject cuesPrefab;
    public float referenceTime;
}

public class MainSequenceManager : MonoBehaviour
{
    // Sequence items array
    public MainSequenceItem[] mainSequenceItems;

    // Scenes
    public string sceneIntro;
    public string sceneOutro;
    public string sceneTransition;
    private string currentScene = "none";

    // Index
    public int currentIndex = 0;
    public int lastIndex = -1;

    // Events
    public UnityEvent onReachedFirst = new UnityEvent();
    public UnityEvent onReachedLast = new UnityEvent();
    public UnityEvent onLeftFirst = new UnityEvent();
    public UnityEvent onLeftLast = new UnityEvent();
    public UnityEventInt onItemChanged = new UnityEventInt();
    public UnityEvent onSceneLoaded = new UnityEvent();

    private void Awake()
    {
        PerformanceGlobalManager.systemInUse = true;
    }

    private void Start()
    {
        int transitionCount = 0;
        foreach(var item in mainSequenceItems)
        {
            if(item.mainSequenceItemType == MainSequenceItemType.Transition)
            {
                item.transitionIndex = transitionCount;
                transitionCount++;
            }
        }
    }

    // Binding the Scene Manager's sceneLoaded action to OnSceneLoaded method on enabling and disabling the component
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Main method to set the current item from index
    public void ItemSet(int index)
    {
        PerformanceGlobalManager.onPreSceneChange.Invoke();

        //Debug.Log("ItemSet(" + index + ") | currentIndex " + currentIndex + " | lastIndex " + lastIndex);

        if(PerformanceGlobalManager.currentControlsPanel)
        {
            Destroy(PerformanceGlobalManager.currentControlsPanel);
        }

        if (lastIndex == index)
        {
            //Debug.Log("Same Index, returning.");
            return;
        }

        currentIndex = index;

        if(index == 0)
        {
            onReachedFirst.Invoke();
        }

        if(index == mainSequenceItems.Length - 1)
        {
            onReachedLast.Invoke();
        }

        if(lastIndex == 0)
        {
            onLeftFirst.Invoke();
        }

        if (lastIndex == mainSequenceItems.Length - 1)
        {
            onLeftLast.Invoke();
        }

        if (currentScene != "none")
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }

        string sceneName = "";
        switch(mainSequenceItems[index].mainSequenceItemType)
        {
            case (MainSequenceItemType.Intro):
                sceneName = sceneIntro;
                break;
            case (MainSequenceItemType.Piece):
                sceneName = "Piece_" + mainSequenceItems[index].sceneName;
                break;
            case (MainSequenceItemType.Transition):
                sceneName = sceneTransition;
                PerformanceGlobalManager.transitionIndex = mainSequenceItems[index].transitionIndex;
                break;
            case (MainSequenceItemType.Outro):
                sceneName = sceneOutro;
                break;
        }

        sceneName = "System_" + sceneName;
        currentScene = "Assets/Scenes/System/" + sceneName + ".unity";
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);

        onItemChanged.Invoke(index);

        lastIndex = index;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);

        if(currentScene != "none")
        {
            SceneManager.SetActiveScene(scene);
        }

        if(scene.name == "System_Main")
        {
            ItemSet(0);
        }

        onSceneLoaded.Invoke();
    }

    public void ItemNext()
    {
        //Debug.Log("ItemNext");
        currentIndex++;
        currentIndex = Mathf.Clamp(currentIndex, 0, mainSequenceItems.Length);
        ItemSet(currentIndex);
    }

    public void ItemPrevious()
    {
        //Debug.Log("ItemPrevious");
        currentIndex--;
        currentIndex = Mathf.Clamp(currentIndex, 0, mainSequenceItems.Length);
        ItemSet(currentIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}