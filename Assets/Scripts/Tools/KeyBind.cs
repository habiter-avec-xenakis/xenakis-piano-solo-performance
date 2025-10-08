using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBind : MonoBehaviour
{
    public KeyCode keycode;
    public UnityEvent trigger = new UnityEvent();

    private void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            trigger.Invoke();
        }
    }
}
