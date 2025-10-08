using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAutoCenter : MonoBehaviour
{
    public Transform avatarReference;
    public Transform targetTransform;
    public bool compensate;

    private void Update()
    {
        if(!avatarReference && !compensate)
        {
            return;
        }

        if(compensate)
        {
            //transform.localPosition = -avatarReference.localPosition;
            //targetTransform.localPosition = -avatarReference.localPosition;
            targetTransform.localPosition = Vector3.Lerp(transform.localPosition, -avatarReference.localPosition, 0.5f);
        }
        else
        {
            targetTransform.localPosition = Vector3.zero;
        }
    }
}