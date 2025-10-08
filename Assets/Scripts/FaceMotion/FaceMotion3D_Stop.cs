using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Smrvfx;


[RequireComponent(typeof(UnityRecieve_FACEMOTION3D))]
public class FaceMotion3D_Stop : MonoBehaviour
{
    private UnityRecieve_FACEMOTION3D fm3d;
    public SkinnedMeshBaker skinnedMeshBaker;

    private void Start()
    {
        fm3d = GetComponent<UnityRecieve_FACEMOTION3D>();
        DontDestroyOnLoad(fm3d.gameObject);
        PerformanceGlobalManager.onPreSceneChange.AddListener(StopFacemotion);
    }

    public void StopFacemotion()
    {
        fm3d.transform.localScale = Vector3.zero;
        PerformanceGlobalManager.onPreSceneChange.RemoveListener(StopFacemotion);
        Destroy(skinnedMeshBaker.gameObject);
        Destroy(this.gameObject);
        //fm3d.StopAllCoroutines();
        //fm3d.StopTCP();
        //StartCoroutine(StopFacemotionDelay(1f));
    }

    //private void OnApplicationQuit()
    //{
    //    StopFacemotion();
    //}

    //IEnumerator StopFacemotionDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //}
}