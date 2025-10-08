using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermaRigManager : MonoBehaviour
{
    public HermaRigControl[] rigControllers;

    public void ActivatePhaseOne()
    {
        rigControllers[0].ApplyPreset(0);
        rigControllers[1].ApplyPreset(1);
        rigControllers[2].ApplyPreset(2);
    }

    public void ActivatePhaseTwo()
    {
        rigControllers[0].ApplyPreset(2);
        rigControllers[1].ApplyPreset(0);
        rigControllers[2].ApplyPreset(1);
    }
}