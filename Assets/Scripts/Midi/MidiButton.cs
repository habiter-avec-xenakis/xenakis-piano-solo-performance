using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MidiJack;

[RequireComponent(typeof(Button))]
public class MidiButton : MonoBehaviour
{
    private Button button;
    public int targetKnobNumber;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        if (knobNumber == targetKnobNumber)
        {
            if (knobValue > 0.5f)
            {
                button.onClick.Invoke();
            }
        }
    }

    void OnEnable()
    {
        MidiMaster.knobDelegate += Knob;
    }

    void OnDisable()
    {
        MidiMaster.knobDelegate -= Knob;
    }
}
