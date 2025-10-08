using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCMyosCausticsControl : MonoBehaviour
{
    public OSCMyos oscMyos;
    public Renderer rendererLeft;
    public Renderer rendererRight;

    private void Update()
    {
        rendererLeft.material.SetFloat("CausticsOffset", oscMyos.lForceMean * 0.05f);
        rendererRight.material.SetFloat("CausticsOffset", oscMyos.rForceMean * 0.05f);

        rendererLeft.material.SetVector("_Speed1", new Vector2(oscMyos.lForceMean * 0.1f, oscMyos.lForceMean * 0.1f));
        rendererLeft.material.SetVector("_Speed2", new Vector2(oscMyos.lForceMean * 0.025f, oscMyos.lForceMean * 0.025f));
        rendererRight.material.SetVector("_Speed1", new Vector2(oscMyos.rForceMean * 0.2f, oscMyos.rForceMean * 0.2f));
        rendererRight.material.SetVector("_Speed2", new Vector2(oscMyos.rForceMean * 0.025f, oscMyos.rForceMean * 0.025f));

        rendererLeft.material.SetFloat("_Multiplier", 5f + oscMyos.lForceMean * 15f);
        rendererRight.material.SetFloat("_Multiplier", 5f + oscMyos.rForceMean * 15f);

        rendererLeft.material.SetFloat("_Power", 1f + oscMyos.lForceMean * 3f);
        rendererRight.material.SetFloat("_Power", 1f + oscMyos.rForceMean * 3f);
    }
}