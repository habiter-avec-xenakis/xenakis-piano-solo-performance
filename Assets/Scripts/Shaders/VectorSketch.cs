using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VectorSketch : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public VisualEffect spriteVfx;
    [Range(0f,1f)]
    public float progression = 0.5f;
    public bool autoProgression = false;

    private void Awake()
    {
        spriteRenderer.material.SetVector("_SpriteExtents", new Vector4(spriteRenderer.bounds.extents.x, spriteRenderer.bounds.extents.y,0f,0f));
        spriteVfx.SetVector2("SpriteExtents", new Vector2(spriteRenderer.bounds.extents.x, spriteRenderer.bounds.extents.y));
        Debug.Log(spriteRenderer.bounds.extents.x + " | " + spriteRenderer.bounds.extents.y);
    }

    private void Update()
    {
        if(autoProgression)
        {
            float autoP = Mathf.PingPong(Time.time * 0.1f, 1f);
            spriteRenderer.material.SetFloat("_Progression", autoP);
            spriteVfx.SetFloat("Progression", autoP);
        }
        else
        {
            spriteRenderer.material.SetFloat("_Progression", progression);
            spriteVfx.SetFloat("Progression", progression);
        }
    }
}