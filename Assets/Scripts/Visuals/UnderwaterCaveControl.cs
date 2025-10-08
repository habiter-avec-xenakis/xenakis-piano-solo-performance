using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UnderwaterCaveControl : MonoBehaviour
{
    public float timeStripes = 1f;

    [Header("Target objects")]
    public VisualEffect vfxDust;
    public Renderer[] rends;

    [Header("Common")]
    public float speed =  1f;
    private float _time;
    public float time
    {
        get { return _time; }
    }

    [Header("Cave")]
    public float strength = 1f;
    public Vector2 caveSize = Vector2.one;

    private void Update()
    {
        _time += Time.deltaTime * speed;
        if (rends != null && rends.Length > 0)
        {
            foreach(var r in rends)
            {
                r.material.SetFloat("Time", _time);
                r.material.SetFloat("Strength", strength);
                r.material.SetFloat("_TimeStripes", timeStripes);
                r.transform.localScale = new Vector3(caveSize.x, caveSize.y, 1f);
            }
        }
        if(vfxDust)
        {
            vfxDust.SetFloat("Speed", speed / 2f);
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetStrength(float value)
    {
        strength = value;
    }

    public void SetCaveSizeX(float value)
    {
        caveSize = new Vector2(value, caveSize.y);
    }
    public void SetCaveSizeY(float value)
    {
        caveSize = new Vector2(caveSize.x, value);
    }
}
