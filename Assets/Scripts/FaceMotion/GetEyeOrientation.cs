using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEyeOrientation : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh mesh;

    public string namePrefix = "blendShape1.";

    public string weightUpNameLeft = "eyeLookUpLeft";
    public string weightDownNameLeft = "eyeLookDownLeft";
    public string weightInNameLeft = "eyeLookInLeft";
    public string weightOutNameLeft = "eyeLookOutLeft";

    private int weightUpIndexLeft;
    private int weightDownIndexLeft;
    private int weightInIndexLeft;
    private int weightOutIndexLeft;

    public string weightUpNameRight = "eyeLookUpRight";
    public string weightDownNameRight = "eyeLookDownRight";
    public string weightInNameRight = "eyeLookInRight";
    public string weightOutNameRight = "eyeLookOutRight";

    private int weightUpIndexRight;
    private int weightDownIndexRight;
    private int weightInIndexRight;
    private int weightOutIndexRight;

    private Vector3 _anglesLeft;
    private Vector3 _anglesRight;

    [Range(0f,1f)]
    public float rotationStrength;

    private Quaternion quatLeft;
    private Quaternion quatRight;

    public float positionMultiplier = 0.1f;

    private Vector3 _positionLeft;
    private Vector3 _positionRight;

    private Vector3 positionLeftOriginal = Vector3.zero;
    private Vector3 positionRightOriginal = Vector3.zero;

    public Transform eyeObjectPosLeft;
    public Transform eyeObjectPosRight;

    public Transform eyeObjectRotLeft;
    public Transform eyeObjectRotRight;

    private void Start()
    {
        if(eyeObjectPosLeft)
        {
            positionLeftOriginal = eyeObjectPosLeft.localPosition;
        }
        if (eyeObjectPosRight)
        {
            positionRightOriginal = eyeObjectPosRight.localPosition;
        }

        mesh = skinnedMeshRenderer.sharedMesh;

        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            var blendShapeName = mesh.GetBlendShapeName(i);

            if (blendShapeName == namePrefix + weightUpNameLeft)
            {
                weightUpIndexLeft = i;
            }

            else if (blendShapeName == namePrefix + weightDownNameLeft)
            {
                weightDownIndexLeft = i;
            }

            else if (blendShapeName == namePrefix + weightInNameLeft)
            {
                weightInIndexLeft = i;
            }

            else if (blendShapeName == namePrefix + weightOutNameLeft)
            {
                weightOutIndexLeft = i;
            }

            else if (blendShapeName == namePrefix + weightUpNameRight)
            {
                weightUpIndexRight = i;
            }

            else if (blendShapeName == namePrefix + weightDownNameRight)
            {
                weightDownIndexRight = i;
            }

            else if (blendShapeName == namePrefix + weightInNameRight)
            {
                weightInIndexRight = i;
            }

            else if (blendShapeName == namePrefix + weightOutNameRight)
            {
                weightOutIndexRight = i;
            }
        }
    }

    private void Update()
    {
        _anglesLeft = GetEyeAngles(
            skinnedMeshRenderer.GetBlendShapeWeight(weightUpIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightDownIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightInIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightOutIndexLeft)
            );

        _anglesRight = GetEyeAngles(
            skinnedMeshRenderer.GetBlendShapeWeight(weightUpIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightDownIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightOutIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightInIndexRight)
            );

         _positionLeft = GetEyeFlatPosition(
            skinnedMeshRenderer.GetBlendShapeWeight(weightUpIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightDownIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightInIndexLeft),
            skinnedMeshRenderer.GetBlendShapeWeight(weightOutIndexLeft),
            positionLeftOriginal
            );

        _positionRight = GetEyeFlatPosition(
            skinnedMeshRenderer.GetBlendShapeWeight(weightUpIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightDownIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightOutIndexRight),
            skinnedMeshRenderer.GetBlendShapeWeight(weightInIndexRight),
            positionRightOriginal
            );

        quatLeft = Quaternion.LookRotation((_positionLeft - positionLeftOriginal) + Vector3.forward * (1f - rotationStrength), Vector3.up);
        quatRight = Quaternion.LookRotation((_positionRight - positionRightOriginal) + Vector3.forward * (1f - rotationStrength), Vector3.up);

        if (eyeObjectPosLeft)
        {
            eyeObjectPosLeft.localPosition = _positionLeft;
        }

        if (eyeObjectPosRight)
        {
            eyeObjectPosRight.localPosition = _positionRight;
        }

        if (eyeObjectRotLeft)
        {
            eyeObjectRotLeft.localRotation = quatLeft;
        }

        if (eyeObjectRotLeft)
        {
            eyeObjectRotRight.localRotation = quatRight;
        }
    }

    private Vector3 GetEyeAngles(float weightUp, float weightDown, float weightRight, float weightLeft)
    {
        Vector3 angles = new Vector3((weightUp - weightDown) / 100f, (weightRight - weightLeft) / 100f, 0) * 180f;
        return angles;
    }

    private Vector3 GetEyeFlatPosition(float weightUp, float weightDown, float weightRight, float weightLeft, Vector3 offset)
    {
        Vector3 eyePosition = offset + new Vector3((weightRight - weightLeft) / 100f, (weightUp - weightDown) / 100f, 0) * positionMultiplier;
        return eyePosition;
    }
}