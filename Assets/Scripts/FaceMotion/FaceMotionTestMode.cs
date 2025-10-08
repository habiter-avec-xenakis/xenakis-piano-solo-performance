using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class FaceMotionTestMode : MonoBehaviour
{
    public BlendShapesWeightsManager recordedBlendShapesManager;

    public void SetTestMode(bool value)
    {
        recordedBlendShapesManager.controlOtherManagers = value;
    }
}