//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(StrangeAttractorMovement))]
//public class StrangeAttractorMovementEditor : Editor
//{
//    private StrangeAttractorMovement sam;
//    private int debugSamples = 2000;

//    private void OnEnable()
//    {
//        sam = (StrangeAttractorMovement)target;
//        if(sam.strangeAttractor == null)
//        {
//            sam.SetStrangeAttractor(sam.strangeAttractorType);
//        }
//    }
//    private void OnDisable()
//    {

//    }

//    public override void OnInspectorGUI()
//    {
//        EditorGUI.BeginChangeCheck();
//        sam.strangeAttractorType = (StrangeAttractorType)EditorGUILayout.EnumPopup("Strange attractor type", sam.strangeAttractorType);
//        if(EditorGUI.EndChangeCheck())
//        {
//            sam.SetStrangeAttractor(sam.strangeAttractorType);
//            SetDebugPoints();
//            EditorUtility.SetDirty(sam);
//        }

//        EditorGUILayout.LabelField(sam.strangeAttractor.GetType().ToString());
//    }

//    private void SetDebugPoints()
//    {
//        Debug.Log("SetDebugPoints()");
//        Vector3 point = Random.insideUnitSphere * 0.1f;
//        sam.debugPoints = new Vector3[debugSamples];
//        for(int i = 0; i < debugSamples; i++)
//        {
//            point = sam.strangeAttractor.GetNewPosition(point, 0.01f);
//            sam.debugPoints[i] = point;
//        }
//    }
//}