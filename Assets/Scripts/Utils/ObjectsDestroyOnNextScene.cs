using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDestroyOnNextScene : MonoBehaviour
{
    public GameObject[] objectsToDestroy;

    private void Start()
    {
        PerformanceGlobalManager.onPreSceneChange.AddListener(DestroyObjects);
    }

    private void DestroyObjects()
    {
        PerformanceGlobalManager.onPreSceneChange.RemoveListener(DestroyObjects);
        foreach(var obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
}
